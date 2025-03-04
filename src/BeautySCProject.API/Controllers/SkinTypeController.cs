
using BeautySCProject.Data.Models.SkinTypeModel;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySCProject.API.Controllers
{
    public class SkinTypeController : BaseAPIController
    {
        private readonly ISkinTypeService _skinTypeService;
        public SkinTypeController (ISkinTypeService skinTypeService)
        {
            _skinTypeService = skinTypeService;
        }
        [HttpGet("get-skin-type-by-id")]
        public async Task<IActionResult> GetSkinType(int skinTypeId)
        {
            var result = await _skinTypeService.GetSkinTypeAsync(skinTypeId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpPut("update-skin-type-by-id")]
        public async Task<IActionResult> Update(SkinTypeUpdateRequestModel request)
        {
            var result = await _skinTypeService.UpdateSkinTypeAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost("create-skin-type")]
        public async Task<IActionResult> Create(SkinTypeCreateRequestModel request)
        {
            var result = await _skinTypeService.CreateSkinTypeAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("get-all-skin-type")]
        public async Task<IActionResult> GetAllSkinType()
        {
            var result = await _skinTypeService.GetAllSkinTypeAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
