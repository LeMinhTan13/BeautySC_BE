using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public float Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? Status { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
