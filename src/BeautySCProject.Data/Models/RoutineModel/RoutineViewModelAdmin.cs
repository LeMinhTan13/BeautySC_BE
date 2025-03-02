using BeautySCProject.Data.ViewModels;
using BeautySCProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.RoutineModel
{
    public class RoutineViewModelAdmin
    {
        public string RoutineName { get; set; }
        public bool Status { get; set; }
        public SkinTypeViewModel SkinType { get; set; }
        public List<RoutineDetailViewModelAdmin> RoutineDetails { get; set; }
    }

    public class RoutineDetailViewModelAdmin
    {
        public string RoutineDetailName { get; set; }
        public List<RoutineStepViewModelAdmin> RoutineSteps { get; set; }
    }

    public class RoutineStepViewModelAdmin
    {
        public int Step { get; set; }
        public string Instruction { get; set; }
        public CategoryRoutineViewModelAdmin Category { get; set; }
    }



    public class CategoryRoutineViewModelAdmin
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
