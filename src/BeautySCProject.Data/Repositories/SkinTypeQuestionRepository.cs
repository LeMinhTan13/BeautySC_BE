using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class SkinTypeQuestionRepository : Repository<SkinTypeQuestion> , ISkinTypeQuestionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        
        public SkinTypeQuestionRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) :base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateSkinTypeQuestionAsync(SkinTypeQuestion skinTypeQuestion)
        {
            try
            {
                await Entities.AddAsync(skinTypeQuestion);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteSkinTypeQuestionBySkinTestIdAsync(int skinTestId)
        {
            try
            {
                var skinTypeQuestion = await Entities.Where(x=>x.SkinTestId == skinTestId).ToListAsync();
                Entities.RemoveRange(skinTypeQuestion);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<SkinTypeQuestion>> GetSkinTypeQuestionsBySkinTestIdAsync(int skinTestId)
        {
            var skinTypeQuestions = await Entities.Where(x => x.SkinTestId == skinTestId).Include(x=>x.SkinTypeAnswers).ToListAsync();
            return skinTypeQuestions;
        }
    }
}
