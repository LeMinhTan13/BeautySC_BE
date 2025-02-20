using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.ProductModel
{
    public class ProductDetail
    {
        public string Size { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public float Discount { get; set; }
    }
}
