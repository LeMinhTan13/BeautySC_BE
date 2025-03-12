using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                Entities.Add(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                Entities.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<bool> ActiveProductAsync(Product product)
        {
            try
            {
                product.Status = Constants.PRODUCT_STATUS_ACTIVE;
                Entities.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> InactiveProductAsync(Product product)
        {
            try
            {
                product.Status = Constants.PRODUCT_STATUS_INACTIVE;
                Entities.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductAsync(int pageIndex, int pageSize, ProductSortContent sorts)
        {
            var query = Entities.AsQueryable();

            // Lọc theo thương hiệu
            if (sorts.Brands.Any())
            {
                query = query.Where(x => sorts.Brands.Contains(x.BrandId));
            }

            // Lọc theo danh mục
            if (sorts.Categories.Any())
            {
                query = query.Where(x => sorts.Categories.Contains(x.CategoryId));
            }

            // Lọc theo loại da
            if (sorts.SkinTypes.Any())
            {
                foreach (var skinType in sorts.SkinTypes)
                {
                    query = query.Where(x => x.ProductSkinTypes.Any(x => x.SkinTypeId == skinType));
                }
            }

            // Lọc theo chức năng
            if (sorts.Functions.Any())
            {
                foreach (var function in sorts.Functions)
                {
                    query = query.Where(x => x.ProductFunctions.Any(x => x.FunctionId == function));
                }
            }

            // Lọc theo thành phần
            if (sorts.Ingredients.Any())
            {
                foreach (var ingredient in sorts.Ingredients)
                {
                    query = query.Where(x => x.ProductIngredients.Any(x => x.IngredientId == ingredient));
                }
            }

            // Lọc theo giá tối thiểu
            if (sorts.MinPrice.HasValue)
            {
                //query = query.Where(x => x.ProductVersions.Any(pv => (pv.Price * (1 - pv.Discount)) >= sorts.MinPrice));
                query = query.Where(x => (x.Price * (decimal)(1 - x.Discount)) >= sorts.MinPrice);
            }

            // Lọc theo giá tối đa
            if (sorts.MaxPrice.HasValue)
            {
                //query = query.Where(x => x.ProductVersions.Any(pv => (pv.Price * (decimal)(1 - pv.Discount)) <= sorts.MaxPrice));
                query = query.Where(x => (x.Price * (decimal)(1 - x.Discount)) <= sorts.MaxPrice);
            }

            // Phân trang và ánh xạ kết quả
            return await query.Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize)
                              .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                              .ToListAsync();
        }

        public async Task<ProductDetailViewModel?> GetProductDetailAsync(int productId)
        {
            return await Entities.ProjectTo<ProductDetailViewModel>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<ProductViewModel?>> GetNewProductAsync()
        {
            return await Entities.Take(10)
                                 .OrderByDescending(x => x.CreatedDate)
                                 .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<ProductViewModel?>> GetBestSellerProductAsync()
        {
            return await Entities.Take(10)
                                 .OrderByDescending(x =>  x.OrderDetails.Where(od => od.ProductId == x.ProductId).Select(od => od.Quantity).Average())
                                 .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }
        public async Task<int> GetNumberOfProductAsync()
        {
            return await Entities.CountAsync();
        }
    }
}
