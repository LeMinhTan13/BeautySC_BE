using AutoMapper;
using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface ISkinTypeQuestionService 
    {
        Task<bool> CreateSkinTypeQuestionAsync(SkinType skinType);
        
        Task<bool> DeleteSkinTypeQuestionBySkinTypeIdAsync(int skinTypeId);

    }
}
