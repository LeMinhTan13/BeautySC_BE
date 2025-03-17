using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public int AccountId { get; set; }

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string? PhoneNumber { get; set; }

        public bool ConfirmedEmail { get; set; }

        public string? Image { get; set; }

        public bool? Status { get; set; }

        public SkinTypeViewModel SkinType { get; set; }
    }
}
