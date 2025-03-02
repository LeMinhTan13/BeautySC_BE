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
        public string RoutineDetailName { get; set; }
        public List<RoutineStepRequest> RoutineSteps { get; set; } = new List<RoutineStepRequest>();
    }
}
