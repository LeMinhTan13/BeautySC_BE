using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.ProductModel;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductSkinTypeService _productSkinTypeService;
        private readonly IProductFunctionService _productFunctionService;
        private readonly IProductIngredientService _productIngredientService;
        private readonly IProductImageService _productImageService;
        private readonly IUnitOfWork _uow;

        public ProductService(IProductRepository productRepository, IMapper mapper, IProductSkinTypeService productSkinTypeService, IProductFunctionService productFunctionService, IProductIngredientService productIngredientService, IProductImageService productImageService, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productSkinTypeService = productSkinTypeService;
            _productFunctionService = productFunctionService;
            _productIngredientService = productIngredientService;
            _productImageService = productImageService;
            _uow = uow;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<MethodResult<string>> CreateProductAsync(ProductCreateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var product = _mapper.Map<Product>(request);

                var checkCrePro = await _productRepository.CreateProductAsync(product);

                if (!checkCrePro)
                {
                    return new MethodResult<string>.Failure("Fail while create product", StatusCodes.Status500InternalServerError);
                }

                if (request.SkinTypes.Any())
                {
                    foreach (var skinTypeId in request.SkinTypes)
                    {
                        var productSkinType = new ProductSkinType
                        {
                            ProductId = product.ProductId,
                            SkinTypeId = skinTypeId
                        };

                        var checkCreProSkinType = await _productSkinTypeService.CreateProductSkinTypeAsync(productSkinType);

                        if (!checkCreProSkinType)
                        {
                            return new MethodResult<string>.Failure("Fail while create product skin type", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                if (request.Functions.Any())
                {
                    foreach (var functionId in request.Functions)
                    {
                        var productFunction = new ProductFunction
                        {
                            ProductId = product.ProductId,
                            FunctionId = functionId
                        };

                        var checkCreProFunc = await _productFunctionService.CreateProductFunctionAsync(productFunction);

                        if (!checkCreProFunc)
                        {
                            return new MethodResult<string>.Failure("Fail while create product function", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                if (request.Ingredients.Any())
                {
                    foreach (var ingredient in request.Ingredients)
                    {
                        var productIngredient = new ProductIngredient
                        {
                            IngredientId = ingredient.IngredientId,
                            Concentration = ingredient.Concentration,
                            ProductId = product.ProductId
                        };

                        var checkCreProIngre = await _productIngredientService.CreateProductIngredientAsync(productIngredient);

                        if (!checkCreProIngre)
                        {
                            return new MethodResult<string>.Failure("Fail while create product ingredient", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                if (request.Images.Any())
                {
                    foreach (var image in request.Images)
                    {
                        var productImage = new ProductImage
                        {
                            Url = image,
                            ProductId = product.ProductId
                        };

                        var checkCreProImg = await _productImageService.CreateProductImageAsync(productImage);

                        if (!checkCreProImg)
                        {
                            return new MethodResult<string>.Failure("Fail while create product image", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Create product succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> UpdateProductAsync(int productId, ProductUpdateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);

                if (product == null)
                {
                    return new MethodResult<string>.Failure("Fail while get product", StatusCodes.Status400BadRequest);
                }

                _mapper.Map(request, product);

                var checkUpPro = await _productRepository.UpdateProductAsync(product);

                if (!checkUpPro)
                {
                    return new MethodResult<string>.Failure("Fail while update product", StatusCodes.Status500InternalServerError);
                }

                var checkDelProSkinType = await _productSkinTypeService.DeleteProductSkinTypesByProductIdAsync(productId);

                if (!checkDelProSkinType)
                {
                    return new MethodResult<string>.Failure("Fail while delete old product skin type", StatusCodes.Status500InternalServerError);
                }

                if (request.SkinTypes.Any())
                {
                    foreach (var skinTypeId in request.SkinTypes)
                    {
                        var productSkinType = new ProductSkinType
                        {
                            ProductId = product.ProductId,
                            SkinTypeId = skinTypeId
                        };

                        var checkCreProSkinType = await _productSkinTypeService.CreateProductSkinTypeAsync(productSkinType);

                        if (!checkCreProSkinType)
                        {
                            return new MethodResult<string>.Failure("Fail while create product skin type", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                var checkDelProFunc = await _productFunctionService.DeleteProductFunctionsByProductIdAsync(productId);

                if (!checkDelProFunc)
                {
                    return new MethodResult<string>.Failure("Fail while delete old product function", StatusCodes.Status500InternalServerError);
                }

                if (request.Functions.Any())
                {
                    foreach (var functionId in request.Functions)
                    {
                        var productFunction = new ProductFunction
                        {
                            ProductId = product.ProductId,
                            FunctionId = functionId
                        };

                        var checkCreProFunc = await _productFunctionService.CreateProductFunctionAsync(productFunction);

                        if (!checkCreProFunc)
                        {
                            return new MethodResult<string>.Failure("Fail while create product function", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                var checkDelProIng = await _productIngredientService.DeleteProductIngredientsByProductIdAsync(productId);

                if (!checkDelProIng)
                {
                    return new MethodResult<string>.Failure("Fail while delete old product ingredient", StatusCodes.Status500InternalServerError);
                }

                if (request.Ingredients.Any())
                {
                    foreach (var ingredient in request.Ingredients)
                    {
                        var productIngredient = new ProductIngredient
                        {
                            IngredientId = ingredient.IngredientId,
                            Concentration = ingredient.Concentration,
                            ProductId = product.ProductId
                        };

                        var checkCreProIngre = await _productIngredientService.CreateProductIngredientAsync(productIngredient);

                        if (!checkCreProIngre)
                        {
                            return new MethodResult<string>.Failure("Fail while create product ingredient", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                var checkDelProImg = await _productImageService.DeleteProductImagesByProductIdAsync(productId);

                if (!checkDelProImg)
                {
                    return new MethodResult<string>.Failure("Fail while delete old product image", StatusCodes.Status500InternalServerError);
                }

                if (request.Images.Any())
                {
                    foreach (var image in request.Images)
                    {
                        var productImage = new ProductImage
                        {
                            Url = image,
                            ProductId = product.ProductId
                        };

                        var checkCreProImg = await _productImageService.CreateProductImageAsync(productImage);

                        if (!checkCreProImg)
                        {
                            return new MethodResult<string>.Failure("Fail while create product image", StatusCodes.Status500InternalServerError);
                        }
                    }                    
                }

                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Update product succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> ActiveProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
            }

            var checkActPro = await _productRepository.ActiveProductAsync(product);

            if (!checkActPro)
            {
                return new MethodResult<string>.Failure("Active product fail", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Active product successfully");
        }

        public async Task<MethodResult<string>> InactiveProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
            }

            var checkActPro = await _productRepository.InactiveProductAsync(product);

            if (!checkActPro)
            {
                return new MethodResult<string>.Failure("Inactive product fail", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Inactive product successfully");
        }

        public async Task<MethodResult<IEnumerable<ProductViewModel>>> GetAllProductAsync(int pageIndex, int pageSize, ProductSortContent sorts)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var result = await _productRepository.GetAllProductAsync(pageIndex, pageSize, sorts);
            return new MethodResult<IEnumerable<ProductViewModel>>.Success(result);
        }

        public async Task<MethodResult<ProductDetailViewModel>> GetProductDetailAsync(int productId)
        {
            var result = await _productRepository.GetProductDetailAsync(productId);
            if (result == null)
            {
                return new MethodResult<ProductDetailViewModel>.Failure("Product not found", StatusCodes.Status404NotFound);
            }
            return new MethodResult<ProductDetailViewModel>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<ProductViewModel>>> GetNewProductAsync()
        {
            var result = await _productRepository.GetNewProductAsync();
            if (result == null)
            {
                return new MethodResult<IEnumerable<ProductViewModel>>.Failure("Product not found", StatusCodes.Status404NotFound);
            }
            return new MethodResult<IEnumerable<ProductViewModel>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<ProductViewModel>>> GetBestSellerProductAsync()
        {
            var result = await _productRepository.GetBestSellerProductAsync();
            if (result == null)
            {
                return new MethodResult<IEnumerable<ProductViewModel>>.Failure("Product not found", StatusCodes.Status404NotFound);
            }
            return new MethodResult<IEnumerable<ProductViewModel>>.Success(result);
        }
    }
}
