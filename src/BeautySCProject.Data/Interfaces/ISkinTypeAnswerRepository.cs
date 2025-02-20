using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ISkinTypeAnswerRepository
    {
        Task<bool> CreateSkinTypeAnswerAsync(SkinTypeAnswer skinTypeAnswer);

        Task<bool> DeleteSkinTypeAnswerBySkinTypeQuestionIdAsync(int questionId);
    }
}
