using Microsoft.Extensions.DependencyInjection;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Repositories;

namespace BeautySCProject.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddTransient<ICustomerRepository, CustomerRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductSkinTypeRepository, ProductSkinTypeRepository>();
            service.AddTransient<IProductFunctionRepository, ProductFunctionRepository>();
            service.AddTransient<IProductIngredientRepository, ProductIngredientRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            service.AddTransient<IShippingAddressRepository, ShippingAddressRepository>();
            service.AddTransient<ITransactionRepository, TransactionRepository>();
            service.AddTransient<IBrandRepository, BrandRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IFunctionRepository, FunctionRepository>();
            service.AddTransient<IIngredientRepository, IngredientRepository>();
            service.AddTransient<IProductImageRepository, ProductImageRepository>();
            service.AddTransient<IVoucherRepository, VoucherRepository>();

            //thành
            service.AddTransient<IRoutineRepository, RoutineRepository>();
            service.AddTransient<ISkinTypeQuestionRepository, SkinTypeQuestionRepository>();
            service.AddTransient<ISkinTypeAnswerRepository, SkinTypeAnswerRepository>();
            service.AddTransient<ISkinTestRepository,SkinTestRepository>();
            service.AddTransient<ISkinTypeRepository, SkinTypeRepository>();
            return service;
        }
    }
}
