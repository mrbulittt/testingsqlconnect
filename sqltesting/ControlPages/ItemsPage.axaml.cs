using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using System;

namespace sqltesting;

public partial class ItemsPage : UserControl
{
    public ItemsPage()
    {
        InitializeComponent();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UserVariableData.selectedItemInMainWidow = null;

        var createAndChangeItem = new CreateAndChangeItem();
        var parent = this.VisualRoot as Window;

        await createAndChangeItem.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }

    private void DeleteButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedItem = button?.DataContext as Item;


        Console.WriteLine((selectedItem == null) ? "User not found" : "User founded");

        if (selectedItem == null) return;

        UserVariableData.selectedItemInMainWidow = selectedItem;

        App.DbContext.Items.Remove(selectedItem);
        App.DbContext.SaveChanges();

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedItem = DataGridItems.SelectedItem as Item;

        if (selectedItem == null) return;

        UserVariableData.selectedItemInMainWidow = selectedItem;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var createAndChangeItem = new CreateAndChangeItem();
        await createAndChangeItem.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }
}