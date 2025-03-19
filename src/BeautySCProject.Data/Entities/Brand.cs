using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string? BrandImage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
