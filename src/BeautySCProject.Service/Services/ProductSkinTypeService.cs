using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class ProductSkinTypeService : IProductSkinTypeService
    {
        private readonly IProductSkinTypeRepository _productSkinTypeRepository;
        private readonly IMapper _mapper;

        public ProductSkinTypeService(IProductSkinTypeRepository productSkinTypeRepository, IMapper mapper)
        {
            _productSkinTypeRepository = productSkinTypeRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateProductSkinTypeAsync(ProductSkinType productSkinType)
        {
            return await _productSkinTypeRepository.CreateProductSkinTypeAsync(productSkinType);
        }

        public async Task<bool> DeleteProductSkinTypesByProductIdAsync(int productId)
        {
            return await _productSkinTypeRepository.DeleteProductSkinTypesByProductIdAsync(productId);
        }

        public async Task<IEnumerable<ProductSkinType>> GetProductSkinTypesByProductIdAsync(int productId)
        {
            return await _productSkinTypeRepository.GetProductSkinTypesByProductIdAsync(productId);
        }

    }
}
