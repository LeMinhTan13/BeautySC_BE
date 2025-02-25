using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeautySCProject.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using BeautySCProject.Data.Models.RoutineModel;
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
        [HttpPost("create-routine")]
        public async Task<IActionResult> CreateRoutine(RoutineCreateRequest routineCreateRequest)
        {
            var result = await _routineService.CreateRoutineAsync(routineCreateRequest);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("update-routine-by-routine-id")]
        public async Task<IActionResult> UpdateRoutineByRoutineId(RoutineUpdateRequest routineUpdateRequest)
        {
            var result = await _routineService.UpdateRoutineAsync(routineUpdateRequest);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-routine")]
        public async Task<IActionResult> GetAllRoutine()
        {
            var result = await _routineService.GetAllRoutineAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
