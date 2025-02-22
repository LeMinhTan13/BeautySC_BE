using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeautySCProject.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
namespace BeautySCProject.API.Controllers
{
    public class RoutineController : BaseAPIController
    {
        private readonly IRoutineService _routineService;
        public RoutineController(IRoutineService routineService) 
        {
            _routineService = routineService;
        }
        [HttpGet("get-routine-by-skin-type-id")]
        
        public async Task<IActionResult> GetRoutine(int skinTypeId)
        {
            var result =  await _routineService.GetRoutineAsync(skinTypeId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
