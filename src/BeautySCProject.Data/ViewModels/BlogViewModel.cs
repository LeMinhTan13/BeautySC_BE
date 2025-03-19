using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string? BlogImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}
