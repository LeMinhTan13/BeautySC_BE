using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Common.Helpers
{
    public static class Constants
    {
        public const string USER_ROLE_CUSTOMER = "Customer";
        public const string USER_ROLE_STAFF = "Staff";
        public const string USER_ROLE_MANAGER = "Manager";
        public const bool USER_STATUS_ACTIVE = true;
        public const bool USER_STATUS_INACTIVE = false;

        public const bool PRODUCT_STATUS_ACTIVE = true;
        public const bool PRODUCT_STATUS_INACTIVE = false;

        public const string ORDER_STATUS_INCART = "InCart";
        public const string ORDER_STATUS_PENDING = "Pending";
        public const string ORDER_STATUS_SHIPPING = "Shipping";
        public const string ORDER_STATUS_COMPLETE = "Complete";
        public const string ORDER_STATUS_CANCEL = "Cancel";
    }
}
