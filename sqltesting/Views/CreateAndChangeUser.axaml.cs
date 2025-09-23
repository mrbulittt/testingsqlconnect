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

        ComboRoles.SelectedItem = UserVariableData.selectedRoleInMainWindow.NameRole;

        if (UserVariableData.seletedUserInMainWindow == null && UserVariableData.SeletedLoginInMainWindow == null)
        {
            DataContext = new User();
            ComboRoles.SelectedItem = UserVariableData.selectedRoleInMainWindow.NameRole;
            return;
        }

        

       DataContext = UserVariableData.seletedUserInMainWindow;

        var userLogin = App.DbContext.Logins.FirstOrDefault(x => x.IdUser == UserVariableData.seletedUserInMainWindow.IdUser);


        if (userLogin != null)
        {
            Login1Text.Text = userLogin.Login1;
            PasswordText.Text = userLogin.Password;
        }




    }

    private async void Button_OnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(FullNameText.Text) || string.IsNullOrEmpty(PhoneNumberText.Text) || ComboRoles.SelectedItem == null || string.IsNullOrEmpty(Login1Text.Text) || string.IsNullOrEmpty(PasswordText.Text) || string.IsNullOrEmpty(DescriptionText.Text))
        {
            return;
        }

       

        if (UserVariableData.seletedUserInMainWindow != null)
        {
                var idUser = UserVariableData.seletedUserInMainWindow.IdUser;
                var thisUser = App.DbContext.Users.FirstOrDefault(x => x.IdUser == idUser);
                
                if (thisUser == null) return;

                var userChange = DataContext as User;
                
                thisUser = userChange;
            thisUser.IdRoleNavigation = ComboRoles.SelectedItem as Role;
                
                

            

            var userLogin = App.DbContext.Logins.FirstOrDefault(x => x.IdUser == idUser);
            if (userLogin != null)
            {
                userLogin.Login1 = Login1Text.Text;
                userLogin.Password = PasswordText.Text;
            }
            else
            {
                var newLogin = new Login
                {
                    Login1 = Login1Text.Text,
                    Password = PasswordText.Text,
                    IdUser = idUser
                };
                App.DbContext.Logins.Add(newLogin);
            }
        }
        else
            {
                //var newUser = new User
                //{
                //    FullName = FullNameText.Text,
                //    PhoneNumber = PhoneNumberText.Text,
                //    Description = DescriptionText.Text,
                //    IdRoleNavigation = ComboRoles.SelectedItem as Role
                //};

                var userCreate = DataContext as User;
                if (userCreate == null) return;

                App.DbContext.Users.Add(userCreate);
                App.DbContext.SaveChanges();



            var newLogin = new Login
            {
                Login1 = Login1Text.Text,
                Password = PasswordText.Text,
                IdUser = userCreate.IdUser
            };

            App.DbContext.Logins.Add(newLogin);
            }

            App.DbContext.SaveChanges();
            this.Close();

    }
}