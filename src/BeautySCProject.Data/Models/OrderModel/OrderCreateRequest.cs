﻿using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.OrderModel
{
    public class OrderCreateRequest
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        //public List<OrderDetailCreateRequest> OrderDetailRequests { get; set; }
        //public int? VoucherId { get; set; } = null!;
    }
}
