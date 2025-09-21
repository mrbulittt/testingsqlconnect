using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using System;


namespace sqltesting;

public partial class BasketPAge : UserControl
{
    public BasketPAge()
    {
        InitializeComponent();
        
    }

    private async void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UserVariableData.selectedBasketInMainWindow = null;

        var createAndChangeBasket = new CreateAndChangeBasket();
        var parent = this.VisualRoot as Window;

        await createAndChangeBasket.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedBasket = BasketDataGrid.SelectedItem as Basket;

        if (selectedBasket == null) return;

        UserVariableData.selectedBasketInMainWindow = selectedBasket;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var createAndChangeBasket = new CreateAndChangeBasket();
        await createAndChangeBasket.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }
    private void DeleteButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedBasket = button?.DataContext as Basket;


        Console.WriteLine((selectedBasket == null) ? "User not found" : "User founded");

        if (selectedBasket == null) return;

        UserVariableData.selectedBasketInMainWindow = selectedBasket;

        App.DbContext.Baskets.Remove(selectedBasket);
        App.DbContext.SaveChanges();

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }
}