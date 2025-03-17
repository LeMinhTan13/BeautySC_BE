using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.BlogModel
{
    public class BlogCreateRequest
    {
        public string BlogTitle { get; set; }
        public string BlogImage { get; set; }
        public bool Status { get; set; }
        public List<BlogDetailCreateRequest> BlogDetails { get; set; } = new List<BlogDetailCreateRequest>();
    }

    public class BlogDetailCreateRequest
    {
        public string BlogDetailTitle { get; set; }
        public string Description { get; set; }
        public string BlogDetailImage { get; set; }
    }
}
