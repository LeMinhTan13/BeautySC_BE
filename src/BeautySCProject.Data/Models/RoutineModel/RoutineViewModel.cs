﻿using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using System.Collections.Generic;

namespace BeautySCProject.Domain.ViewModels
{
    public class RoutineViewModel
    {
        public string RoutineName { get; set; }
        public bool Status { get; set; }
        public SkinTypeViewModel SkinType { get; set; }
        public List<RoutineDetailViewModel> RoutineDetails { get; set; }

    }

    public class RoutineDetailViewModel
    {
        public string RoutineDetailName { get; set; }
        public List<RoutineStepViewModel> RoutineSteps { get; set; }
    }

    public class RoutineStepViewModel
    {
        public int Step { get; set; }
        public string Instruction { get; set; }
        public CategoryViewModel Category { get; set; }
    }



    public class CategoryViewModel
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

        public List<ProductImageRoutineViewModel> ProductImages { get; set; } = new List<ProductImageRoutineViewModel>();
    }
    public class ProductImageRoutineViewModel
    {
        public String Url { get; set; }
    }
    public class RoutineGetAllViewModel
    {
        public int RoutineId { get; set; }
        public string RoutineName { get; set; } = null!;
        public string SkinTypeName { get; set; } = null!;
    }
}