using System.Collections.Generic;
using System.Linq;
using sqltesting.Data;

namespace sqltesting.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public List<User> Users { get; set; }
    public List<Login> Logins { get; set; }
    public List<Role> Roles { get; set; }
    public List<Item> Items { get; set; }
    public List<Basket> Baskets { get; set; }


    public MainWindowViewModel()
    {
        RefreshData();
    }

    public void RefreshData()
    {
        var UsersFromDb = App.DbContext.Users.ToList();
        Users = UsersFromDb;
        var LoginsFromDb = App.DbContext.Logins.ToList();
        Logins = LoginsFromDb;
        var RolesFromDb = App.DbContext.Roles.ToList();
        Roles = RolesFromDb;
        var ItemsFromDb = App.DbContext.Items.ToList();
        Items = ItemsFromDb;
        var BasketFromDb = App.DbContext.Baskets.ToList();
        Baskets = BasketFromDb;
        OnPropertyChanged(nameof(Users));
        OnPropertyChanged(nameof(Logins));
        OnPropertyChanged(nameof(Roles));
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(Baskets));
    }
}