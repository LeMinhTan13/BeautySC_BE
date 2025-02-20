using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class RoutineStep
{
    public int RoutineStepId { get; set; }

    public int Step { get; set; }

    public string Instruction { get; set; } = null!;

    public int RoutineDetailId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual RoutineDetail RoutineDetail { get; set; } = null!;
}
