using BeautySCProject.Data.Models.SkinTypeQuestionModel;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautySCProject.API.Controllers
{
   
    public class SkinTypeQuestionController : BaseAPIController
    {
        private readonly ISkinTypeQuestionService _skinTypeQuestionService;
        public SkinTypeQuestionController(ISkinTypeQuestionService skillTypeQuestionService)
        {
            _skinTypeQuestionService = skillTypeQuestionService;
        }

        [HttpPost("{skinTestId}")]
        public async Task<IActionResult> CreateQuestion(int skinTestId, [FromBody] List<SkinTypeQuestionModel> questions)
        {
            if (questions == null)
            {
                return BadRequest("questions cannot be null.");
            }

            var result = await _skinTypeQuestionService.CreateSkinTypeQuestionAsync(skinTestId, questions);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
