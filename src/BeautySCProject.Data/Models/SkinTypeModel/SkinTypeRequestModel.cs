using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.SkinTypeModel
{
    public class SkinTypeCreateRequestModel
    {
        public string SkinTypeName { get; set; } = null!;

        public int Priority { get; set; }
    }
    public class SkinTypeUpdateRequestModel
    {
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; } = null!;

        public int Priority { get; set; }
    }

}
