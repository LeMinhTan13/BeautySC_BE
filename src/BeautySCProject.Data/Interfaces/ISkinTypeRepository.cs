using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ISkinTypeRepository
    {
        Task<List<SkinTypeAnswer>> GetSkinTypeAnswersAsync(List<int> answerIds);
        Task<SkinType> GetSkinTypeByIdAsync(int skinTypeId);
    }

}
