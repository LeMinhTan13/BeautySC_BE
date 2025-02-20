using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class RoutineDetail
{
    public int RoutineDetailId { get; set; }

    public string RoutineDetailName { get; set; } = null!;

    public int RoutineId { get; set; }

    public virtual Routine Routine { get; set; } = null!;

    public virtual ICollection<RoutineStep> RoutineSteps { get; set; } = new List<RoutineStep>();
}
