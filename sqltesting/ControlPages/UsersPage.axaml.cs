using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using sqltesting.Views;
using System;
using System.Linq;

namespace sqltesting;

public partial class UsersPage : UserControl
{
    public UsersPage()
    {
        InitializeComponent();
        DataGridUsers.ItemsSource = App.DbContext.Logins.ToList();
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        UserVariableData.SeletedLoginInMainWindow = null;
        UserVariableData.seletedUserInMainWindow = null;

        var parent = this.VisualRoot as Window;
        var createdAndChangeUserWindow = new CreateAndChangeUser();
        await createdAndChangeUserWindow.ShowDialog(parent);

        DataGridUsers.ItemsSource = App.DbContext.Logins.ToList();

    }

    private async void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {

       var selectedLogin = DataGridUsers.SelectedItem as Login;
        if (selectedLogin == null) return;

        UserVariableData.SeletedLoginInMainWindow = selectedLogin;
        UserVariableData.seletedUserInMainWindow = selectedLogin.IdUserNavigation; 

        var parent = this.VisualRoot as Window;
        var createdAndChangeUserWindow = new CreateAndChangeUser();
        await createdAndChangeUserWindow.ShowDialog(parent);

        DataGridUsers.ItemsSource = App.DbContext.Logins.ToList();
    }
    private async void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedUser = button?.DataContext as User;
        

        Console.WriteLine((selectedUser == null) ? "User not found" : "User founded");

        if (selectedUser == null) return;

        UserVariableData.seletedUserInMainWindow = selectedUser;

        App.DbContext.Users.Remove(selectedUser);
        App.DbContext.SaveChanges();

        DataGridUsers.ItemsSource = App.DbContext.Users.ToList();
    }
}