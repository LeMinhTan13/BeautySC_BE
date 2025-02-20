using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ProductIngredient
{
    public int ProductIngredientId { get; set; }

    public decimal? Concentration { get; set; }

    public int ProductId { get; set; }

    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
