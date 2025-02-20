using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.ProductModel
{
    public class ProductUpdateRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        public string Size { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public float Discount { get; set; }

        public string? Summary { get; set; }

        public ulong IsRecommended { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public List<int> SkinTypes { get; set; } = new List<int>();

        public List<IngredientDetail> Ingredients { get; set; } = new List<IngredientDetail>();

        public List<int> Functions { get; set; } = new List<int>();

        public List<ProductDetail> Details { get; set; }
    }
}
