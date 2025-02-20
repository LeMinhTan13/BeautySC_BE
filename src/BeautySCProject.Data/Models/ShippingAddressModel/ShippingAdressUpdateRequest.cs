using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.ShippingAddressModel
{
    public class ShippingAdressUpdateRequest
    {
        [Required]
        public int ShippingAddressId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; } 
    }
}
