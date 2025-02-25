using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.Models.SkinTestModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface ISkinTestService
    {
        Task<MethodResult<string>> CreateSkinTestAsync(SkinTestCreateRequest request);
        Task<MethodResult<SkinTestViewModel>> GetSkinTestAsync(int skinTestId);
        Task<MethodResult<string>> UpdateSkinTestAsync(SkinTestUpdateRequest request);
        Task<SkinTest> GetSkinTestByIdAsync (int skinTestId);
        Task<MethodResult<object>> DetermineSkinTypeAsync(int customerId, ListUserAnswerModel model);
        Task<MethodResult<IEnumerable<SkinTestGetAllViewModel>>> GetAllSkinTestAsync();
    }
}
