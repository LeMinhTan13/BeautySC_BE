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
    public class FeedbackRepository: Repository<Feedback>, IFeedbackRepository
    {
        private readonly IMapper _mapper;
        private readonly BeautyscDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public FeedbackRepository(IMapper mapper, BeautyscDbContext dbContext,IUnitOfWork unitOfWork) : base(dbContext)
        {
        
            _mapper = mapper;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateFeedbackAsync(Feedback feedback)
        {
            try
            {
                 Entities.Add(feedback);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public async Task<IEnumerable<FeedbackViewModel>> GetFeedbackByCustomerIdAndProductIdAsync(int customerId, int productId)
        {
            var feedbacks = await Entities
                    .Include(x => x.Customer)
                    .Where(x => x.CustomerId == customerId && x.ProductId == productId)
                    .ToListAsync();
            return _mapper.Map<IEnumerable<FeedbackViewModel>>(feedbacks);
        }

        public async Task<IEnumerable<FeedbackViewModel>> GetFeedbackByCustomerIdAsync(int customerId)
        {
            var feedbacks = await Entities
                    .Include(x => x.Customer)
                    .Where(x => x.CustomerId == customerId)
                    .ToListAsync();
            return _mapper.Map<IEnumerable<FeedbackViewModel>>(feedbacks);
        }

        public async Task<FeedbackViewModel> GetFeedbackByFeedbackAsync(int feedbackId)
        {
            var feedback = await Entities
                    .Include(x => x.Customer)
                    .Where(x => x.FeedbackId == feedbackId )
                    .FirstOrDefaultAsync();
            return _mapper.Map<FeedbackViewModel>(feedback);
        }
        public async Task<Feedback?> GetFeedbackAsync(int feedbackId)
        {
            var feedback = await Entities.FindAsync(feedbackId);
            await _unitOfWork.SaveChangesAsync();
            return feedback;
        }
        public async Task<IEnumerable<FeedbackViewModel>> GetFeedbackByProductIdAsync(int productId)
        {
            var feedbacks = await Entities
                    .Include(x => x.Customer)
                    .Where(x => x.ProductId == productId)
                    .ToListAsync();
            return _mapper.Map<IEnumerable<FeedbackViewModel>>(feedbacks);
        }
        public async Task<bool> UpdateFeedbackAsync(Feedback feedback)
        {
            Entities.Update(feedback);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
