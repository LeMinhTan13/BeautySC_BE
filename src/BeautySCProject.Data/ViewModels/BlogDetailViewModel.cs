using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class BlogDetailViewModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string? BlogImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AccountId { get; set; }

        public bool Status { get; set; }

        public List<Content> BlogDetails { get; set; }
    }

    public class Content
    {
        public int BlogDetailId { get; set; }

        public string BlogDetailTitle { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? BlogDetailImage { get; set; }
    }
}
