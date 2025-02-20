using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Summary { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public float Rating { get; set; }

        public string ProductImage { get; set; }
    }
}
