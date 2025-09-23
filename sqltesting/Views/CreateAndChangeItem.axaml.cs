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


        if (UserVariableData.selectedItemInMainWidow == null) return;
        DataContext = UserVariableData.selectedItemInMainWidow;
    }
    private void SaveUserButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(ItemNameText.Text) || string.IsNullOrEmpty(ItemDescText.Text) || string.IsNullOrEmpty(ItemCostText.Text)) return;

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