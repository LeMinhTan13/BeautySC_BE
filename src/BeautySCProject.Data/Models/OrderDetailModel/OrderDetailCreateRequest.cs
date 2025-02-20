using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.OrderDetailModel
{
    public class OrderDetailCreateRequest
    {
        [Required]
        public int ProductVersionId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
