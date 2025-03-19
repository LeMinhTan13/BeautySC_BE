using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.BlogModel
{
    public class BlogUpdateRequest
    {
        public string BlogTitle { get; set; }
        public string BlogImage { get; set; }
        public bool Status { get; set; }
        public List<BlogDetailUpdateRequest> BlogDetails { get; set; } = new List<BlogDetailUpdateRequest>();
    }

    public class BlogDetailUpdateRequest
    {
        public int BlogDetailId { get; set; }
        public string BlogDetailTitle { get; set; }
        public string Description { get; set; }
        public string BlogDetailImage { get; set; }
    }
}
