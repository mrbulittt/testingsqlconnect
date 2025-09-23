using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using System;
using System.Linq;

namespace sqltesting.Views;

public partial class CreateAndChangeUser : Window
{
    public CreateAndChangeUser()
    {
        InitializeComponent();
        ComboRoles.ItemsSource = App.DbContext.Roles.ToList();

        if (UserVariableData.seletedUserInMainWindow != null)
        {
            var login = App.DbContext.Logins
                .FirstOrDefault(l => l.IdUser == UserVariableData.seletedUserInMainWindow.IdUser);

            if (login != null)
            {
                DataContext = login;

                if (login.IdUserNavigation != null && login.IdUserNavigation.IdRoleNavigation != null)
                {
                    ComboRoles.SelectedItem = login.IdUserNavigation.IdRoleNavigation;
                }
            }
        }
        else
        {
            DataContext = new Login()
            {
                IdUserNavigation = new User()
            };
        }

    }

    private async void Button_OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Login1Text.Text) ||
                string.IsNullOrWhiteSpace(PasswordText.Text) ||
                ComboRoles.SelectedItem == null)
        {
            return;
        }

        var selectedRole = ComboRoles.SelectedItem as Role;
        var thisLogin = DataContext as Login;

        if (thisLogin == null) return;

        if (UserVariableData.seletedUserInMainWindow != null)
        {

            thisLogin.Login1 = Login1Text.Text;
            thisLogin.Password = PasswordText.Text;

            if (thisLogin.IdUserNavigation != null)
            {
                thisLogin.IdUserNavigation.IdRole = selectedRole.IdRole;
            }

            App.DbContext.Update(thisLogin);
            if (thisLogin.IdUserNavigation != null)
            {
                App.DbContext.Update(thisLogin.IdUserNavigation);
            }
        }
        else
        {
            if (thisLogin.IdUserNavigation == null)
            {
                thisLogin.IdUserNavigation = new User();
            }

            thisLogin.IdUserNavigation.IdRole = selectedRole.IdRole;

            App.DbContext.Logins.Add(thisLogin);
        }

        App.DbContext.SaveChanges();
        this.Close();
    }
}
