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

        /*public Task<MethodResult<string>> CreateSkinTypeAsync(SkinTypeViewModel request)
        {
            throw new NotImplementedException();
        }*/

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

       /* public Task<MethodResult<string>> UpdateRoutineAsync(SkinTypeViewModel request)
        {
            throw new NotImplementedException();
        }*/

       
    }
}
