using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.AuthenticationModel
{
    public class AuthenticationRepsonse
    {
        public AccessToken AccessToken { get; set; } = new AccessToken();
        public string RefreshToken { get; set; } = string.Empty;

    }
}
