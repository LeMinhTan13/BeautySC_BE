using AutoMapper;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Data.Models.SkinTypeModel;
using BeautySCProject.Data.Entities;

namespace BeautySCProject.Service.Services
{
    public class SkinTypeService : ISkinTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISkinTypeRepository _skinTypeRepository;

        public SkinTypeService(ISkinTypeRepository skinTypeRepository, IMapper mapper)
        {
            _skinTypeRepository = skinTypeRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<string>> CreateSkinTypeAsync(SkinTypeCreateRequestModel request)
        {
            var skinType = _mapper.Map<SkinType>(request);
            var checkCreateSkinType = await _skinTypeRepository.CreateSkinTypeAsync(skinType);
            if (!checkCreateSkinType)
            {
                return new MethodResult<string>.Failure("Can't create Skin Type!", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Create successfully!");
        }

        public async Task<MethodResult<IEnumerable<SkinTypeViewModel>>> GetAllSkinTypeAsync()
        {
            var result = await _skinTypeRepository.GetAllSkinTypeAsync();
            return new MethodResult<IEnumerable<SkinTypeViewModel>>.Success(result);
        }

        public async Task<MethodResult<SkinTypeViewModel>> GetSkinTypeAsync(int skinTypeId)
        {
            var skintype = await _skinTypeRepository.GetSkinTypeByIdAsync(skinTypeId);
            var result = _mapper.Map<SkinTypeViewModel>(skintype);
            if (result == null) // Kiểm tra danh sách rỗng
            {
                return new MethodResult<SkinTypeViewModel>.Failure("Can't found Skin Type!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<SkinTypeViewModel>.Success(result);
        }

        public async Task<MethodResult<string>> UpdateSkinTypeAsync(SkinTypeUpdateRequestModel request)
        {
            var skintype = await _skinTypeRepository.GetSkinTypeByIdAsync(request.SkinTypeId);
            if (skintype == null)
            {
                return new MethodResult<string>.Failure("Can't found Skin Type!", StatusCodes.Status404NotFound);
            }
            skintype.Priority = request.Priority;
            skintype.SkinTypeName = request.SkinTypeName;
            var checkUpdateSkinType = await _skinTypeRepository.UpdateSkinTypeAsync(skintype);
            if(!checkUpdateSkinType)
            {
                return new MethodResult<string>.Failure("Can't update Skin Type!", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Update successfully!");
        }

       
    }
}
