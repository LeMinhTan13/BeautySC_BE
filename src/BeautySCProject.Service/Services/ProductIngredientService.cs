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
    public class ProductIngredientService : IProductIngredientService
    {
        private readonly IProductIngredientRepository _productIngredientRepository;
        private readonly IMapper _mapper;

        public ProductIngredientService(IProductIngredientRepository productIngredientRepository, IMapper mapper)
        {
            _productIngredientRepository = productIngredientRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateProductIngredientAsync(ProductIngredient productIngredient)
        {
            return await _productIngredientRepository.CreateProductIngredientAsync(productIngredient);
        }

        public async Task<bool> DeleteProductIngredientsByProductIdAsync(int productId)
        {
            return await _productIngredientRepository.DeleteProductIngredientsByProductIdAsync(productId);
        }
    }
}
