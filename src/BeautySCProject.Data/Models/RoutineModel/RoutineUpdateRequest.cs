using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.RoutineModel
{
    public class RoutineUpdateRequest
    {
        public int RoutineId { get; set; }
        public string RoutineName { get; set; }
        public bool Status { get; set; }
        public int SkinTypeId { get; set; }
        public List<RoutineDetailUpdateRequest> RoutineDetails { get; set; } = new List<RoutineDetailUpdateRequest>();
    }
    public class RoutineDetailUpdateRequest
    {
        public int RoutineDetailId { get; set; }
        public string RoutineDetailName { get; set; }
        public List<RoutineStepUpdateRequest> RoutineSteps { get; set; } = new List<RoutineStepUpdateRequest>();
    }
    public class RoutineStepUpdateRequest
    {
        public int RoutineStepId { get; set; }
        public int Step { get; set; }
        public string Instruction { get; set; }
        public int CategoryId { get; set; }
    }
}
