using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class SkinTestRepository : Repository<SkinTest>, ISkinTestRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private readonly IMapper _mapper;

        public SkinTestRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext; 
            _mapper = mapper;
        }

        public async Task<bool> CreateSkinTestAsync(SkinTest skinTest)
        {
            try
            {
                Entities.Add(skinTest);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<SkinTestViewModel> GetSkinTestAsync(int skinTestId)
        {
            var skinTest = await Entities.Include(x => x.SkinTypeQuestions).ThenInclude(x => x.SkinTypeAnswers)
                .FirstOrDefaultAsync(x => x.SkinTestId == skinTestId);
            var skinTestViewModel = _mapper.Map<SkinTestViewModel>(skinTest);// xài profile mà chưa hiểu lắm nên xài này luôn
            return skinTestViewModel;
        }

        public async Task<SkinTest> GetSkinTestByIdAsync(int skinTestId)
        {
            var skinTest = await Entities.Include(x => x.SkinTypeQuestions).ThenInclude(x => x.SkinTypeAnswers)
                .FirstOrDefaultAsync(x => x.SkinTestId == skinTestId);
            var skinTestMap = _mapper.Map<SkinTest>(skinTest);// xài profile mà chưa hiểu lắm nên xài này luôn
            return skinTestMap;
        }

        public async Task<bool> UpdateSkinTestAsync(SkinTest skinTest)
        {
            try
            { 
                Entities.Update(skinTest);
                await _unitOfWork.SaveChangesAsync();   
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<List<SkinTypeAnswer>> GetSkinTypeAnswersAsync(List<int> answerIds)
        {
            // Truy vấn danh sách câu trả lời từ cơ sở dữ liệu dựa trên danh sách answerIds
            return await _dbContext.SkinTypeAnswers
                .Where(answer => answerIds.Contains(answer.SkinTypeAnswerId))
                .Include(answer => answer.SkinType)
                .ToListAsync();
        }

        public async Task<SkinType> GetSkinTypeByIdAsync(int skinTypeId)
        {
            // Truy vấn loại da từ cơ sở dữ liệu theo SkinTypeId
            return await _dbContext.SkinTypes
                .Where(st => st.SkinTypeId == skinTypeId)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SkinTestViewModel>> GetAllSkinTestAsync()
        {
            return await Entities
                .Include(x => x.SkinTypeQuestions)
                    .ThenInclude(q => q.SkinTypeAnswers)
                .Select(x => new SkinTestViewModel
                {
                    SkinTestId = x.SkinTestId,
                    SkinTestName = x.SkinTestName,
                    Status = x.Status,
                    SkinTypeQuestions = x.SkinTypeQuestions
                        .Where(q => q.SkinTypeAnswers.Any()) 
                        .Select(q => new SkinTypeQuestionViewModel
                        {
                            SkinTypeQuestionId = q.SkinTypeQuestionId,
                            Description = q.Description,
                            SkinTypeAnswers = q.SkinTypeAnswers
                                .Select(a => new SkinTypeAnswerViewModel
                                {
                                    SkinTypeAnswerId = a.SkinTypeAnswerId,
                                    Description = a.Description,
                                    SkinTypeId = a.SkinTypeId
                                }).ToList()
                        }).ToList()
                }).ToListAsync();
        }


    }

}
