using BeautySCProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.RoutineModel
{
    public class RoutineCreateRequest
    {
        public string RoutineName { get; set; } = null!;
        public int SkinTypeId { get; set; }
        public bool Status { get; set; }
        public List<RoutineDetailCreateRequest> RoutineDetails { get; set; } = new List<RoutineDetailCreateRequest>();

    }
    public class RoutineDetailCreateRequest
    {
        public string RoutineDetailName { get; set; }
        public List<RoutineStepRequest> RoutineSteps { get; set; } = new List<RoutineStepRequest>();
    }
    public class RoutineStepRequest
    {
        public int Step { get; set; }
        public string Instruction { get; set; }
        public int CategoryId { get; set; }
    }

}
