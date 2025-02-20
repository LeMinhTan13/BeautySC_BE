using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string VoucherName { get; set; } = null!;

    public string VoucherCode { get; set; } = null!;

    public float VoucherValue { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
