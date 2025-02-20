using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.AuthenticationModel
{
    public class RefreshRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
