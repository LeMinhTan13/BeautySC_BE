﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.Configuration
{
    public class MailConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }   
        public string FromEmail { get; set; }
        public string Password { get; set; }
    }
}
