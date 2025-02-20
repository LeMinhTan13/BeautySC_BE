using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.CustomerModel
{
    public class SearchCustomerList
    {
        public string? FullName { get; set; } = null!;
        public SortContent? SortContent { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = int.MaxValue;
    }

    public class SortContent
    {
        public SortCustomerByEnum sortCustomerBy { get; set; }
        public SortCustomerTypeEnum sortCustomerType { get; set; }
    }

    public enum SortCustomerByEnum
    {
        CustomerId = 1,
        FullName = 2
    }
    public enum SortCustomerTypeEnum
    {
        Ascending = 1,
        Descending = 2,
    }
}
