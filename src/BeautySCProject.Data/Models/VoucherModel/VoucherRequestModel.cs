using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.VoucherModel
{
    public class VoucherCreateRequestModel
    {
        public string VoucherName { get; set; }
        public string VoucherCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public decimal MinimumPurchase { get; set; }
    }
    public class VoucherUpdateRequestModel
    {
        public int VoucherId { get; set; }
        public string VoucherName { get; set; }
        public string VoucherCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public decimal MinimumPurchase { get; set; }

    }
}
