using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Domain.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class RoutineService: IRoutineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoutineRepository _routineRepository;
        public RoutineService(IRoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }

        public async Task<MethodResult<RoutineViewModel>> GetRoutineAsync(int skinTypeId)
        {
            var result = await _routineRepository.GetRoutinesBySkinTypeAsync(skinTypeId);
            if (result == null) // Kiểm tra danh sách rỗng
            {
                return new MethodResult<RoutineViewModel>.Failure("Can't found routine!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<RoutineViewModel>.Success(result);
        }
    }
}
