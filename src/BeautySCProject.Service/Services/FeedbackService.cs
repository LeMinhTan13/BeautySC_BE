using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.FeedbackModel;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
        }
        public async Task<MethodResult<string>> CreateFeedbackAsync(int customerId, FeedbackCreateRequestModel request)
        {
            var feedback = _mapper.Map<Feedback>(request);
            feedback.CustomerId = customerId;
            var checkCreateFeedback = await _feedbackRepository.CreateFeedbackAsync(feedback);
            if (!checkCreateFeedback)
            {
                return new MethodResult<string>.Failure("Can't create feedback",StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Create successfully");
        }

        public async Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByCustomerIdAndProductIdAsync(int customerId, int productId)
        {
            var result = await _feedbackRepository.GetFeedbackByCustomerIdAndProductIdAsync(customerId,productId);
            return new MethodResult<IEnumerable<FeedbackViewModel>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByCustomerIdAsync(int customerId)
        {
            var result = await _feedbackRepository.GetFeedbackByCustomerIdAsync(customerId);
            return new MethodResult<IEnumerable<FeedbackViewModel>>.Success(result);
        }

        public async Task<MethodResult<FeedbackViewModel>> GetFeedbackByFeedbackAsync(int feedbackId)
        {
            var result = await _feedbackRepository.GetFeedbackByFeedbackAsync(feedbackId);
            return new MethodResult<FeedbackViewModel>.Success(result);
        }
        
        public async Task<MethodResult<IEnumerable<FeedbackViewModel>>> GetFeedbackByProductIdAsync(int productId)
        {
            var result = await _feedbackRepository.GetFeedbackByProductIdAsync(productId);
            return new MethodResult<IEnumerable<FeedbackViewModel>>.Success(result);
        }

        public async Task<MethodResult<string>> UpdateFeedbackAsync(int customerId, FeedbackUpdateRequestModel request)
        {
            try
            {

                var feedback = await _feedbackRepository.GetFeedbackAsync(request.FeedbackId);
                if (feedback == null)
                {
                    return new MethodResult<string>.Failure("Cann't found feedback!", StatusCodes.Status204NoContent);
                }
                if (feedback.CustomerId != customerId) 
                {
                    return new MethodResult<string>.Failure("You are not accept to update!", StatusCodes.Status304NotModified);
                }
                feedback.Rating = request.Rating;
                feedback.Status = request.Status;
                feedback.CreatedDate = request.CreatedDate;
                feedback.Comment = request.Comment;
                var checkUpdateFeedback = await _feedbackRepository.UpdateFeedbackAsync(feedback);
                if (!checkUpdateFeedback)
                {
                    return new MethodResult<string>.Failure("Cann't update feedback!", StatusCodes.Status500InternalServerError);
                }
                return new MethodResult<string>.Success("Feedback updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure($"Error: {ex.Message}", StatusCodes.Status500InternalServerError);
            }


        }

    }
}
