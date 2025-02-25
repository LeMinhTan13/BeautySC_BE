using BeautySCProject.Data.Entities;
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
    }
    public class RoutineGetAllViewModel
    {
        public int RoutineId { get; set; }
        public string RoutineName { get; set; } = null!;
        public string SkinTypeName { get; set; } = null!; 
    }

}
