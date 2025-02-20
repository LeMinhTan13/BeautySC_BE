using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ShippingAddress
{
    public int ShippingAddressId { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsDefault { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
