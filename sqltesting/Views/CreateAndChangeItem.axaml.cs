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
        DataContext = new MainWindowViewModel();

        if (UserVariableData.selectedItemInMainWidow != null)
        {
            ItemNameText.Text = UserVariableData.selectedItemInMainWidow.NameItem;
            ItemDescText.Text = UserVariableData.selectedItemInMainWidow.DescriptionItem;
            ItemCostText.Text = UserVariableData.selectedItemInMainWidow.Cost.ToString();
        }
    }
    private void SaveUserButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(ItemNameText.Text) || string.IsNullOrEmpty(ItemDescText.Text) || string.IsNullOrEmpty(ItemCostText.Text)) return;

        if (UserVariableData.selectedItemInMainWidow != null)
        {
            var idItem = UserVariableData.selectedItemInMainWidow.IdItem;
            var thisItem = App.DbContext.Items.FirstOrDefault(x => x.IdItem == idItem);

            if (thisItem == null) return;

            thisItem.NameItem = ItemNameText.Text;
            thisItem.DescriptionItem = ItemDescText.Text;
            thisItem.Cost = int.Parse(ItemCostText.Text);

           

        }
        else
        {
            var newItem = new Item()
            {
                NameItem = ItemNameText.Text,
                DescriptionItem = ItemDescText.Text,
                Cost = int.Parse(ItemCostText.Text),
            };

            App.DbContext.Items.Add(newItem);

            App.DbContext.SaveChanges();

        }

        App.DbContext.SaveChanges();
        this.Close();
    }
}