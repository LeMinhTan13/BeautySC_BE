using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using System.Collections.Generic;

namespace BeautySCProject.Domain.ViewModels
{
    public class RoutineViewModel
    {
        public int RoutineId { get; set; }
        public string RoutineName { get; set; }
        public bool Status { get; set; }
        public SkinTypeViewModel SkinType { get; set; }
        public List<RoutineDetailViewModel> RoutineDetails { get; set; }

    }

    public class RoutineDetailViewModel
    {
        public int RoutineDetailId { get; set; }
        public string RoutineDetailName { get; set; }
        public List<RoutineStepViewModel> RoutineSteps { get; set; }
    }

    public class RoutineStepViewModel
    {
        public int RoutineStepId { get; set; }
        public int Step { get; set; }
        public string Instruction { get; set; }
        public CategoryRoutineViewModel Category { get; set; }
    }



    public class CategoryRoutineViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductRoutineViewModel> Products { get; set; }
    }
    public class ProductRoutineViewModel
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Summary { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
        public List<ProductSkinTypeRoutineViewModel> ProductSkinTypes   { get; set; }

        public List<ProductImageRoutineViewModel> ProductImages { get; set; } = new List<ProductImageRoutineViewModel>();
    }
    public class ProductImageRoutineViewModel
    {
        public String Url { get; set; }
    }
    public class RoutineGetAllViewModel
    {
        public int RoutineId { get; set; }
        public bool Status { get; set; }
        public string RoutineName { get; set; } = null!;
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; } = null!;
    }
    public class ProductSkinTypeRoutineViewModel
    {
        public int ProductId { get; set; }
        public int SkinTypeId { get; set; }
    }
}