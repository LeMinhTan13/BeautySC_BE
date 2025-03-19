using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class BlogDetail
{
    public int BlogDetailId { get; set; }

    public int BlogId { get; set; }

    public string BlogDetailTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? BlogDetailImage { get; set; }

    public virtual Blog Blog { get; set; } = null!;
}
