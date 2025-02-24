using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.ProductModel
{
    public class ProductCreateRequest
    {
        [Required]
        public string ProductName { get; set; } = null!;

        public string? Summary { get; set; }

        public string Size { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public bool IsRecommended { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public List<int> SkinTypes { get; set; } = new List<int>();

        public List<IngredientDetail> Ingredients { get; set; } = new List<IngredientDetail>();

        public List<int> Functions { get; set; } = new List<int>();

        public List<string> Images { get; set; } = new List<string>();
    }
}
