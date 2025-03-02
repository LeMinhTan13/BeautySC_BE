using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ISkinTypeQuestionRepository
    {
        Task<bool> CreateSkinTypeQuestionAsync(List<SkinTypeQuestion> skinTypeQuestions);
        Task<IEnumerable<SkinTypeQuestion>> GetSkinTypeQuestionsBySkinTestIdAsync(int skinTestId);
        Task<bool> DeleteSkinTypeQuestionBySkinTestIdAsync(int skinTestId);
    }
}
