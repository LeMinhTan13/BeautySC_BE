﻿using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    internal class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public ProductImageRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductImageAsync(ProductImage productImage)
        {
            try
            {
                await Entities.AddAsync(productImage);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductImagesByProductIdAsync(int productId)
        {
            try
            {
                var productImages = await Entities.Where(x => x.ProductId == productId).ToListAsync();
                Entities.RemoveRange(productImages);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesByProductIdAsync(int productId)
        {
            return await Entities.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
