using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ProductFunction
{
    public int ProductFunctionId { get; set; }

    public int ProductId { get; set; }

    public int FunctionId { get; set; }

    public virtual Function Function { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
