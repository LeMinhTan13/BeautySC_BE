using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    internal class ProductFunctionService : IProductFunctionService
    {
        private readonly IProductFunctionRepository _productFunctionRepository;
        private readonly IMapper _mapper;

        public ProductFunctionService(IProductFunctionRepository productFunctionRepository, IMapper mapper)
        {
            _productFunctionRepository = productFunctionRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateProductFunctionAsync(ProductFunction productFunction)
        {
            return await _productFunctionRepository.CreateProductFunctionAsync(productFunction);
        }

        public async Task<bool> DeleteProductFunctionsByProductIdAsync(int productId)
        {
            return await _productFunctionRepository.DeleteProductFunctionsByProductIdAsync(productId);
        }

        public async Task<IEnumerable<ProductFunction>> GetProductFunctionByProductIdAsync(int productId)
        {
            return await _productFunctionRepository.GetProductFunctionByProductIdAsync(productId);
        }
    }
}
