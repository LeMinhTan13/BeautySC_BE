using BeautySCProject.Data.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }       

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public string PaymentMethodName { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public List<OrderDetailViewModel> Details { get; set; } = null!;
    }

    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductImage { get; set; } = null!;

        public string Size { get; set; } = null!;

        public int Quantity { get; set; }
       
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
    }
}
