using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.SkinTypeModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface ISkinTypeService
    {
        Task<MethodResult<SkinTypeViewModel>> GetSkinTypeAsync(int skinTypeId);
        Task<MethodResult<string>> CreateSkinTypeAsync(SkinTypeCreateRequestModel request);
          
        Task<MethodResult<string>> UpdateSkinTypeAsync(SkinTypeUpdateRequestModel request);
        Task<MethodResult<IEnumerable<SkinTypeViewModel>>> GetAllSkinTypeAsync();
    }
}
