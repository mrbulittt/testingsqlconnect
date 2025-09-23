using sqltesting.Data;

namespace sqltesting.Models;

public class UserVariableData
{
    public static User seletedUserInMainWindow { get; set; }
    public static Login SeletedLoginInMainWindow { get; set; }
    public static Item selectedItemInMainWidow { get; set; }
    public static Role selectedRoleInMainWindow { get; set; }
    public static Basket selectedBasketInMainWindow { get; set; }

}