using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class ProfileViewModel
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string? PhoneNumber { get; set; }

        public bool ConfirmedEmail { get; set; }

        public string? Image { get; set; }

        public SkinTypeViewModel SkinType { get; set; }
    }
}
