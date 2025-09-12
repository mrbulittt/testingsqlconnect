using System;
using System.Collections.Generic;

namespace sqltesting.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Description { get; set; }

    public int? IdRole { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
