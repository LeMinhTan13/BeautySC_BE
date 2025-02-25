using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Domain.ViewModels;
using BeautySCProject.Data.Models.RoutineModel;

namespace BeautySCProject.Service.Interfaces
{
    public interface IRoutineService
    {
        Task<MethodResult<RoutineViewModel>> GetRoutineAsync(int skinTypeId); 
        Task<MethodResult<string>> CreateRoutineAsync(RoutineCreateRequest request);
        Task<MethodResult<string>> UpdateRoutineAsync(RoutineUpdateRequest request);
        Task<MethodResult<IEnumerable<RoutineGetAllViewModel>>> GetAllRoutineAsync();
    }
}
