using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using sqltesting.Data;
using sqltesting.Models;
using sqltesting.ViewModels;
using System.Linq;

namespace sqltesting;

public partial class CreateAndChangeBasket : Window
{
    public CreateAndChangeBasket()
    {
        InitializeComponent();
        ComboUsers.ItemsSource = App.DbContext.Users.ToList();
        ComboItems.ItemsSource = App.DbContext.Items.ToList();

        if (UserVariableData.selectedBasketInMainWindow != null)
        {
            DataContext = UserVariableData.selectedBasketInMainWindow;

            ComboUsers.SelectedItem = App.DbContext.Users
                .FirstOrDefault(u => u.IdUser == UserVariableData.selectedBasketInMainWindow.IdUser);
            ComboItems.SelectedItem = App.DbContext.Items
                .FirstOrDefault(i => i.IdItem == UserVariableData.selectedBasketInMainWindow.IdItem);

            CountText.Text = UserVariableData.selectedBasketInMainWindow.Count.ToString();
        }
        else
        {
            DataContext = new Basket();
        }
    }

    private async void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ComboUsers.SelectedItem == null || ComboItems.SelectedItem == null ||
            string.IsNullOrWhiteSpace(CountText.Text) || !int.TryParse(CountText.Text, out int count))
        {
            return;
        }

        var thisBasket = DataContext as Basket;
        if (thisBasket == null) return;

        thisBasket.IdUserNavigation = ComboUsers.SelectedItem as User;
        thisBasket.IdItemNavigation = ComboItems.SelectedItem as Item;
        thisBasket.Count = UserVariableData.selectedBasketInMainWindow.Count;

        if (UserVariableData.selectedBasketInMainWindow != null)
        {

            App.DbContext.Update(thisBasket);
        }
        else
        {

            App.DbContext.Baskets.Add(thisBasket);
        }

        App.DbContext.SaveChanges();
        this.Close();

    }
}