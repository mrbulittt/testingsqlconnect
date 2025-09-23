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
        DataGridUsers.ItemsSource = App.DbContext.Users.ToList();
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        UserVariableData.seletedUserInMainWindow = null;

        var createAndChangeUserWindow = new CreateAndChangeUser();
        var parent = this.VisualRoot as Window;

        await createAndChangeUserWindow.ShowDialog(parent);

        DataGridUsers.ItemsSource = App.DbContext.Users.ToList();

    }

    private async void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {

        var selectedUser = DataGridUsers.SelectedItem as User;

        if (selectedUser == null) return;

        UserVariableData.seletedUserInMainWindow = selectedUser;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var createAndChangeUserWindow = new CreateAndChangeUser();
        await createAndChangeUserWindow.ShowDialog(parent);

        DataGridUsers.ItemsSource = App.DbContext.Users.ToList();
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