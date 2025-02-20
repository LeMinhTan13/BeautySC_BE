using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.VnPayModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<MethodResult<IEnumerable<BrandViewModel>>> GetBrandsAsync()
        {
            var result = await _brandRepository.GetBrandsAsync();
            return new MethodResult<IEnumerable<BrandViewModel>>.Success(result);
        }
    }
}
