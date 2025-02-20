using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ProductSkinType
{
    public int ProductSkinTypeId { get; set; }

    public int ProductId { get; set; }

    public int SkinTypeId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SkinType SkinType { get; set; } = null!;
}
