using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public int CustomerId { get; set; }

    public int? VoucherId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Voucher? Voucher { get; set; }
}
