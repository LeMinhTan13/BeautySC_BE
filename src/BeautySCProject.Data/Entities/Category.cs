using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<RoutineStep> RoutineSteps { get; set; } = new List<RoutineStep>();
}
