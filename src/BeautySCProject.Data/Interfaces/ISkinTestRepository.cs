using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ISkinTestRepository
    {
        Task<bool> CreateSkinTestAsync(SkinTest skinTest);
        Task<SkinTestViewModel> GetSkinTestAsync(int skinTestId);

        Task<bool> UpdateSkinTestAsync(SkinTest skinTest);
        Task<SkinTest> GetSkinTestByIdAsync(int skinTestId);

        Task<List<SkinTypeAnswer>> GetSkinTypeAnswersAsync(List<int> answerIds);
        Task<SkinType> GetSkinTypeByIdAsync(int skinTypeId);
    }
}
