using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;

namespace BeautySCProject.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection service)
        {
            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<IRefreshTokenService, RefreshTokenService>();
            service.AddTransient<ITokenGenerator, TokenGenerator>();
            service.AddTransient<ITokenValidator, TokenValidator>();
            service.AddTransient<ICustomerService, CustomerService>();
            service.AddTransient<ISmtpClient, SmtpClient>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IProductSkinTypeService, ProductSkinTypeService>();
            service.AddTransient<IProductFunctionService, ProductFunctionService>();
            service.AddTransient<IProductIngredientService, ProductIngredientService>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IOrderDetailService, OrderDetailService>();
            service.AddTransient<IShippingAddressService, ShippingAddressService>();
            service.AddTransient<ITransactionService, TransactionService>();
            service.AddTransient<IPaymentService, PaymentService>();
            service.AddTransient<IBrandService, BrandService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IFunctionService, FunctionService>();
            service.AddTransient<IIngredientService, IngredientService>();
            service.AddTransient<IProductImageService, ProductImageService>();
            service.AddTransient<IVoucherService, VoucherService>();

            //thành
            service.AddTransient<IRoutineService, RoutineService>();
            service.AddTransient<ISkinTypeQuestionService, SkinTypeQuestionService>();
            service.AddTransient<ISkinTestService, SkinTestService>();
            service.AddTransient<ISkinTypeService, SkinTypeService>();
            service.AddTransient<IFeedbackService, FeedbackService>();
            return service;
        }
    }
}
