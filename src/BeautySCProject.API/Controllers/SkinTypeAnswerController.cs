using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautySCProject.API.Controllers
{
    public class SkinTypeAnswerController : BaseAPIController
    {
        /*private readonly ISkinTypeAnswerService _skinTypeAnswerService;
        public SkinTypeAnswerController(ISkinTypeAnswerService skinTypeAnswerService)
        {
            _skinTypeAnswerService = skinTypeAnswerService;
        }
        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateAnswer(int questionId, [FromBody] AnswerModel answerModel)
        {
            if (answerModel == null)
            {
                return BadRequest("AnswerModel cannot be null.");
            }

            var result = await _skinTypeAnswerService.UpdateAnswerAsync(questionId, answerModel);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpPost("{questionId}")]
        public async Task<IActionResult> CreateAnswer(int questionId, [FromBody] List<AnswerModel> answerModel)
        {
            if (answerModel == null)
            {
                return BadRequest("AnswerModel cannot be null.");
            }

            var result = await _skinTypeAnswerService.CreateAnswerAsync(questionId, answerModel);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }*/
    }
}
