using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string? BlogImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AccountId { get; set; }

    public bool Status { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<BlogDetail> BlogDetails { get; set; } = new List<BlogDetail>();
}
