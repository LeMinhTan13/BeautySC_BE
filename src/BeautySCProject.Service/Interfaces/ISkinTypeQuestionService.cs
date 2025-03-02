using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.SkinTypeQuestionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface ISkinTypeQuestionService 
    {
        Task<MethodResult<string>> CreateSkinTypeQuestionAsync(int skinTestId, List<SkinTypeQuestionModel> questions);
        
        Task<bool> DeleteSkinTypeQuestionBySkinTypeIdAsync(int skinTypeId);

    }
}
