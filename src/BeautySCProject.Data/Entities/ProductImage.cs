using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public string? Url { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
