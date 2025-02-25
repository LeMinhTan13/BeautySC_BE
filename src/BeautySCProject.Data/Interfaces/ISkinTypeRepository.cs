using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ISkinTypeRepository
    {
        Task<SkinType?> GetSkinTypeByIdAsync(int skinTypeId);
      /*  Task<bool> CreateSkinTypeAsync(SkinType skinType);
        Task<bool> UpdateSkinTypeAsync(SkinType skinType);*/
        Task<IEnumerable<SkinTypeViewModel>> GetAllSkinTypeAsync();
    }

}
