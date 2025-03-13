using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Summary { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }        

        public BrandViewModel Brand { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }

        public List<SkinTypeViewModel> SkinTypes { get; set; }
        
        public List<FunctionViewModel> Functions { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; }

        public List<FeedbackViewModel> Feedbacks { get; set; }
    }
  
    public class ProductImageViewModel
    {
        public int ProductImageId { get; set; }

        public string ProductImage { get; set; }
    }
    
    public class BrandViewModel
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }
    }
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
    public class FunctionViewModel
    {
        public int FunctionId { get; set; }

        public string FunctionName { get; set; }
    }
    public class IngredientViewModel
    {
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public decimal? Concentration { get; set; }
    }
}
