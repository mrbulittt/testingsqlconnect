using System;
using System.Collections.Generic;

namespace sqltesting.Data;

public partial class Login
{
    public int IdLogin { get; set; }

    public string? Login1 { get; set; }

    public string? Password { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
