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
    public class SkinTypeAnswerRepository : Repository<SkinTypeAnswer> , ISkinTypeAnswerRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        
        public SkinTypeAnswerRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext) 
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task<bool> CreateSkinTypeAnswerAsync(SkinTypeAnswer skinTypeAnswer)
        {
            try
            {
                await Entities.AddAsync(skinTypeAnswer);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSkinTypeAnswerBySkinTypeQuestionIdAsync(int skinTypeQuestionId) 
        {
            try
            { 
                var skinTypeAnswer = await Entities.Where(x=>x.SkinTypeQuestionId == skinTypeQuestionId).ToListAsync();
                Entities.RemoveRange(skinTypeAnswer);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
