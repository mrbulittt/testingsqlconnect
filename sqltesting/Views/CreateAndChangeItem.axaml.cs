using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using System.Linq;

namespace sqltesting;

public partial class CreateAndChangeItem : Window
{
    public CreateAndChangeItem()
    {
        InitializeComponent();

        if (UserVariableData.selectedItemInMainWidow == null)
        {
            DataContext = new Item();
            return;
        }
        DataContext = UserVariableData.selectedItemInMainWidow;
    }
    private void SaveUserButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ItemNameText.Text) || string.IsNullOrWhiteSpace(ItemCostText.Text))
        {
            return;
        }

        if (!int.TryParse(ItemCostText.Text, out int cost))
        {
            return;
        }

        if (UserVariableData.selectedItemInMainWidow != null)
        {
            var idItem = UserVariableData.selectedItemInMainWidow.IdItem;
            var thisItem = App.DbContext.Items.FirstOrDefault(x => x.IdItem == idItem);

            if (thisItem == null) return;

            var itemChange = DataContext as Item;

            thisItem = itemChange;
        }

        else
        {
            var itemCreate = DataContext as Item;
            App.DbContext.Items.Add(itemCreate);
            App.DbContext.SaveChanges();
        }

        App.DbContext.SaveChanges();
        this.Close();
    }
}