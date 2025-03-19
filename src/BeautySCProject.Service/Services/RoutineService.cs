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
            var routine = await _routineRepository.GetRoutinesBySkinTypeAsync(skinTypeId);
            var mappedRoutine = _mapper.Map<RoutineViewModel>(routine);

            foreach (var routineDetail in mappedRoutine.RoutineDetails)
            {
                foreach (var routineStep in routineDetail.RoutineSteps)
                {
                    routineStep.Category.Products = routineStep.Category.Products
                        .Where(p => p.ProductSkinTypes.Any(pst => pst.SkinTypeId == skinTypeId) && p.IsRecommended)
                        .ToList();
                }
            }

            foreach (var routineDetail in mappedRoutine.RoutineDetails)// lọc 6 sản phẩm daàu tiên
            {
                foreach (var routineStep in routineDetail.RoutineSteps)
                {
                    if (routineStep.Category != null)
                    {
                        routineStep.Category.Products = routineStep.Category.Products
                            .Take(6) 
                            .ToList();
                    }
                }
            }
            if (mappedRoutine == null) 
            {
                return new MethodResult<RoutineViewModel>.Failure("Can't found routine!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<RoutineViewModel>.Success(mappedRoutine);
        }

        public async Task<MethodResult<string>> UpdateRoutineAsync(RoutineUpdateRequest request)
        {
            try
            {
                var existingRoutine = await _routineRepository.GetRoutinesByRoutineIdAsync(request.RoutineId);
                if (existingRoutine == null)
                {
                    return new MethodResult<string>.Failure("Routine not found", StatusCodes.Status404NotFound);
                }

                existingRoutine.RoutineName = request.RoutineName;
                existingRoutine.Status = request.Status;
                existingRoutine.SkinTypeId = request.SkinTypeId;

                //routineDetails
                foreach (var routineDetail in request.RoutineDetails)
                {
                    var existingDetail = existingRoutine.RoutineDetails
                        .FirstOrDefault(d => d.RoutineDetailId == routineDetail.RoutineDetailId);

                    if (existingDetail != null)
                    {
                        existingDetail.RoutineDetailName = routineDetail.RoutineDetailName;

                        // routineSteps
                        foreach (var routineStep in routineDetail.RoutineSteps)
                        {
                            var existingStep = existingDetail.RoutineSteps
                                .FirstOrDefault(s => s.RoutineStepId == routineStep.RoutineStepId);

                            if (existingStep != null)
                            {
                                existingStep.Step = routineStep.Step;
                                existingStep.Instruction = routineStep.Instruction;
                                existingStep.CategoryId = routineStep.CategoryId;
                            }
                            else
                            {
                                existingDetail.RoutineSteps.Add(new RoutineStep
                                {
                                    Step = routineStep.Step,
                                    Instruction = routineStep.Instruction,
                                    CategoryId = routineStep.CategoryId
                                });
                            }
                        }
                    }
                    else
                    {
                        var newDetail = new RoutineDetail
                        {
                            RoutineDetailName = routineDetail.RoutineDetailName,
                            RoutineSteps = routineDetail.RoutineSteps.Select(s => new RoutineStep
                            {
                                Step = s.Step,
                                Instruction = s.Instruction,
                                CategoryId = s.CategoryId
                            }).ToList()
                        };
                        existingRoutine.RoutineDetails.Add(newDetail);
                    }
                }

                var checkUpRoutine = await _routineRepository.UpdateRoutineAsync(existingRoutine);
                if (!checkUpRoutine)
                {
                    return new MethodResult<string>.Failure("Fail while updating routine!", StatusCodes.Status500InternalServerError);
                }

                return new MethodResult<string>.Success("Routine updated successfully");
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
            var mappedRoutine = _mapper.Map<RoutineViewModel>(routine);
            foreach (var routineDetail in mappedRoutine.RoutineDetails)// lọc 6 sản phẩm daàu tiên
            {
                foreach (var routineStep in routineDetail.RoutineSteps)
                {
                    if (routineStep.Category != null)
                    {
                        routineStep.Category.Products = routineStep.Category.Products
                            .Take(6)
                            .ToList();
                    }
                }
            }
            if (mappedRoutine == null) 
            {
                return new MethodResult<RoutineViewModel>.Failure("Can't found routine!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<RoutineViewModel>.Success(mappedRoutine);
        }
        public async Task<MethodResult<RoutineViewModelAdmin>> GetRoutineByRoutineIdForAdminAsync(int routineId)
        {
            var routine = await _routineRepository.GetRoutinesByRoutineIdForAminAsync(routineId);
            var mappedRoutine = _mapper.Map<RoutineViewModelAdmin>(routine);
            
            if (mappedRoutine == null)
            {
                return new MethodResult<RoutineViewModelAdmin>.Failure("Can't found routine!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<RoutineViewModelAdmin>.Success(mappedRoutine);
        }
    }
}
