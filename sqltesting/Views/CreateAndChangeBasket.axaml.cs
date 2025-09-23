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

        if (UserVariableData.selectedBasketInMainWindow == null) return;
        ComboUsers.SelectedItem = UserVariableData.selectedBasketInMainWindow.IdUserNavigation;
        ComboItems.SelectedItem = UserVariableData.selectedBasketInMainWindow.IdItemNavigation;
        CountText.Text = UserVariableData.selectedBasketInMainWindow.Count.ToString(); 
    }

    private async void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ComboUsers.SelectedItem == null || ComboItems.SelectedItem == null || string.IsNullOrEmpty(CountText.Text)) return;

        if(UserVariableData.selectedBasketInMainWindow != null)
        {
            var idBasket = UserVariableData.selectedBasketInMainWindow.IdBasket;
            var thisBasket = App.DbContext.Baskets.FirstOrDefault(x => x.IdBasket == idBasket);
            if (thisBasket == null) return;

            thisBasket.IdUserNavigation = ComboUsers.SelectedItem as User;
            thisBasket.IdItemNavigation = ComboItems.SelectedItem as Item;
            thisBasket.Count = int.Parse(CountText.Text);
            

        }
        else
        {
            var newBasket = new Basket
            {
                IdUserNavigation = ComboUsers.SelectedItem as User,
                IdItemNavigation = ComboItems.SelectedItem as Item,
                Count = int.Parse(CountText.Text)
            };

            App.DbContext.Baskets.Add(newBasket);
            App.DbContext.SaveChanges();
        }

        App.DbContext.SaveChanges();
        this.Close();
            
    }
}