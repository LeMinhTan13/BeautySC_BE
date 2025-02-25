
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
