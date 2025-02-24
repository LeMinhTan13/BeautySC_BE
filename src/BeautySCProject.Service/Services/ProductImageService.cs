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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageService(IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateProductImageAsync(ProductImage productImage)
        {
            return await _productImageRepository.CreateProductImageAsync(productImage);
        }

        public async Task<bool> DeleteProductImagesByProductIdAsync(int productId)
        {
            return await _productImageRepository.DeleteProductImagesByProductIdAsync(productId);
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesByProductIdAsync(int productId)
        {
            return await _productImageRepository.GetProductImagesByProductIdAsync(productId);
        }
    }
}
