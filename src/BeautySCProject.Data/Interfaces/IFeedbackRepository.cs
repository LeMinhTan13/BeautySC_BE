using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<bool> CreateFeedbackAsync(Feedback feedback);
        Task<IEnumerable<FeedbackViewModel>> GetFeedbackByCustomerIdAndProductIdAsync(int customerId, int productId);
        Task<IEnumerable<FeedbackViewModel>> GetFeedbackByCustomerIdAsync(int customerId);
        Task<FeedbackViewModel> GetFeedbackByFeedbackAsync(int feedbackId);
        Task<Feedback> GetFeedbackAsync(int feedbackId);
        Task<IEnumerable<FeedbackViewModel>> GetFeedbackByProductIdAsync(int productId);
        Task<bool> UpdateFeedbackAsync(Feedback feedback);
    }
}
