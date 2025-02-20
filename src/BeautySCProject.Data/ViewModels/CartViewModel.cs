using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class CartViewModel
    {
        public int OrderDetailId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
