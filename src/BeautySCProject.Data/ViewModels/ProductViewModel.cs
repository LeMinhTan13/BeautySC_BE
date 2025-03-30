using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Summary { get; set; }

        public int Quantity { get; set; }

        public bool? Status { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public float Rating { get; set; }

        public string ProductImage { get; set; }

        public BrandViewModel Brand { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<SkinTypeViewModel> SkinTypes { get; set; }

        public List<FunctionViewModel> Functions { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
