using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurcusProject.Data.Models.AuthenticationModel
{
    public class VerifyNewEmailRequest
    {
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
    }
}
