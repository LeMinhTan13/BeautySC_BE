using BeautySCProject.Data.Models.SkinTestModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    public class SkinTestController : BaseAPIController
    {
        private readonly ISkinTestService _skinTestService;
        public SkinTestController(ISkinTestService skinTestService)
        {
            _skinTestService = skinTestService;
        }
        [HttpPost("determine-skin-type")]
        [Authorize]
        public async Task<ActionResult<object>> DetermineSkinType([FromBody] ListUserAnswerModel request)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _skinTestService.DetermineSkinTypeAsync(customerId,request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpPost("create-skin-test")]
        [Authorize]
        public async Task<IActionResult> CreateSkinTest(SkinTestCreateRequest request)
        {
            var result = await _skinTestService.CreateSkinTestAsync(request);
            return result.Match(
                (l,c) => Problem(detail:l,statusCode:c),
                Ok
            );
        }

        [HttpGet("get-skin-test")]
        public async Task<IActionResult> GetSkinTest(int skinTestId)
        {
            var result = await _skinTestService.GetSkinTestAsync(skinTestId);
            return result.Match(
                (l,c) => Problem(detail:l, statusCode:c),
                Ok
            );
        }

        [HttpPut("update-skin-test")]
        [Authorize]
        public async Task<IActionResult> UpdateSkinTest(SkinTestUpdateRequest request)
        {
            var result = await _skinTestService.UpdateSkinTestAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("get-all-skin-test")]
        
        public async Task<IActionResult> GetAllSkinTest()
        {
            var result = await _skinTestService.GetAllSkinTestAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        
    };
}
