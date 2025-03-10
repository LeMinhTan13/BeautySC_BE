using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.Category
{
    public class CategoryCountProductModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
    }
}
