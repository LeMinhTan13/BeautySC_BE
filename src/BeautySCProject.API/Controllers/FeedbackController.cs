using BeautySCProject.Data.Models.FeedbackModel;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{

    public class FeedbackController : BaseAPIController
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("create-feedback")]
        [Authorize]
        public async Task<IActionResult> Create(FeedbackCreateRequestModel request)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _feedbackService.CreateFeedbackAsync(customerId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-feedback-by-customerId")]
        [Authorize]
        public async Task<IActionResult> GetByCustomerId(int customerId)// for admin and customer
        {
            var result = await _feedbackService.GetFeedbackByCustomerIdAsync(customerId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-feedback-by-customerId-and-productId")]
        [Authorize]
        public async Task<IActionResult> GetByCustomerIdAndProductId(int customerId, int productId)
        {
            var result = await _feedbackService.GetFeedbackByCustomerIdAndProductIdAsync(customerId,productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-feedback-by-productId")]
        [Authorize]
        public async Task<IActionResult> GetByProductId(int productId)// for show to user
        {
            var result = await _feedbackService.GetFeedbackByProductIdAsync(productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-feedback-by-feedbackId")]
        [Authorize]
        public async Task<IActionResult> GetByFeedbackId(int feedbackId)//for user
        {
            var result = await _feedbackService.GetFeedbackByFeedbackAsync(feedbackId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("update-feedback")]
        [Authorize]
        public async Task<IActionResult> Update(FeedbackUpdateRequestModel request)//for user
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _feedbackService.UpdateFeedbackAsync(customerId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
