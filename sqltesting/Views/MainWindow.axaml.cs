using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;

namespace sqltesting.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainControl.Content = new UsersPage();
    }

    private async void Button_Users(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new UsersPage();
    }

    private async void Button_Items(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new ItemsPage();

    }

    private async void Button_Basket(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainControl.Content = new BasketPAge();


    }
}