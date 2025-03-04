using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class FeedbackViewModel
    {
        public int FeedbackId { get; set; }
        public float Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool? Status { get; set; }

        public int ProductId { get; set; }
        public CustomerFeedbackViewModel Customer {  get; set; }
    }
    public class CustomerFeedbackViewModel
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Image { get; set; }
    }
}
