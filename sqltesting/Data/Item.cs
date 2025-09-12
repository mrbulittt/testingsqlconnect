using System;
using System.Collections.Generic;

namespace sqltesting.Data;

public partial class Item
{
    public int IdItem { get; set; }

    public string? NameItem { get; set; }

    public int? Cost { get; set; }

    public string? DescriptionItem { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
