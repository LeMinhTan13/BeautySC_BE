using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class VoucherViewModel
    {
        public int VoucherId { get; set; }

        public string VoucherName { get; set; } = null!;

        public string VoucherCode { get; set; } = null!;

        public string? Description { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? MinimumPurchase { get; set; }


    }
}
