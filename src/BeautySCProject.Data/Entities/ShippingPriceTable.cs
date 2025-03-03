using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class ShippingPriceTable
{
    public int ShippingPriceTableId { get; set; }

    public float FromWeight { get; set; }

    public float? ToWeight { get; set; }

    public decimal InRegion { get; set; }

    public decimal OutRegion { get; set; }

    public decimal? Pir { get; set; }

    public decimal? Por { get; set; }
}
