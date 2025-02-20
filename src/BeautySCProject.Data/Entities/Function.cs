using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Function
{
    public int FunctionId { get; set; }

    public string FunctionName { get; set; } = null!;

    public virtual ICollection<ProductFunction> ProductFunctions { get; set; } = new List<ProductFunction>();
}
