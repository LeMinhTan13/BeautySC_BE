using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
    }
}
