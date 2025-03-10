using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.FeedbackModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<MethodResult<string>> CreateFeedbackAsync(int customerId,FeedbackCreateRequestModel request);
        Task<MethodResult<string>> UpdateFeedbackAsync(int customerId,FeedbackUpdateRequestModel request);
        Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByCustomerIdAsync(int customerId);
        Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByCustomerIdAndProductIdAsync(int customerId,int productId);
        Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByProductIdAsync( int productId);
        Task<MethodResult<FeedbackViewModel>> GetFeedbackByFeedbackAsync(int feedback);
        
        Task<MethodResult<string>> GetNumberOfFeedbackAsync(int productId);
    }
}
