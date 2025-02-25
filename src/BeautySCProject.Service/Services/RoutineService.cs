using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.RoutineModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoutineRepository _routineRepository;

        public RoutineService(IRoutineRepository routineRepository,IMapper mapper)
        {
            _routineRepository = routineRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<string>> CreateRoutineAsync(RoutineCreateRequest request)
        {
            try
            {
                var routine = _mapper.Map<Routine>(request);
                var checkCreRoutine = await _routineRepository.CreateRoutineAsync(routine);
                if (!checkCreRoutine)
                {
                    return new MethodResult<string>.Failure("Fail while create routine", StatusCodes.Status500InternalServerError);
                }
                return new MethodResult<string>.Success("Create Routine successfully!");
            }
            catch (Exception ex) 
            {
                return new MethodResult<string>.Failure($"Error: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
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

        public async Task<MethodResult<string>> UpdateRoutineAsync(RoutineUpdateRequest request)
        {
            try
            {
                var routineRe = request;
                var routine = _mapper.Map<Routine>(request);

                var checkUpRoutine = await _routineRepository.UpdateRoutineAsync(routine);
                if (!checkUpRoutine)
                {
                    return new MethodResult<string>.Failure("Fail while update routine!", StatusCodes.Status500InternalServerError);
                }
                return new MethodResult<string>.Success("routine updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure($"Error: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<MethodResult<IEnumerable<RoutineGetAllViewModel>>> GetAllRoutineAsync()
        {
            var result = await _routineRepository.GetAllRoutineAsync();
            return new MethodResult<IEnumerable<RoutineGetAllViewModel>>.Success(result);
        }

        public async Task<MethodResult<RoutineViewModel>> GetRoutineByRoutineIdAsync(int routineId)
        {
            var routine = await _routineRepository.GetRoutinesByRoutineIdAsync(routineId);
            var result = _mapper.Map<RoutineViewModel>(routine);
            if (result == null) // Kiểm tra danh sách rỗng
            {
                return new MethodResult<RoutineViewModel>.Failure("Can't found routine!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<RoutineViewModel>.Success(result);
        }
    }
}
