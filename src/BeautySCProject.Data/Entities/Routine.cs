using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Routine
{
    public int RoutineId { get; set; }

    public string RoutineName { get; set; } = null!;

    public bool Status { get; set; }

    public int SkinTypeId { get; set; }

    public virtual ICollection<RoutineDetail> RoutineDetails { get; set; } = new List<RoutineDetail>();

    public virtual SkinType SkinType { get; set; } = null!;
}
