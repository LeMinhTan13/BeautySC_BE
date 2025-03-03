using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Summary { get; set; }

    public string Size { get; set; } = null!;

    public decimal Price { get; set; }

    public float Weight { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsRecommended { get; set; }

    public bool? Status { get; set; }

    public int BrandId { get; set; }

    public int CategoryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductFunction> ProductFunctions { get; set; } = new List<ProductFunction>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

    public virtual ICollection<ProductSkinType> ProductSkinTypes { get; set; } = new List<ProductSkinType>();
}
