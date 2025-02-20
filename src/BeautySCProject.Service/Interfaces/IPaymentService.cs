using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.VnPayModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<MethodResult<string>> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
        Task<MethodResult<string>> CreateTransactionAsync(Order order, VnPaymentResponseModel response);
        Task<MethodResult<string>> CreatePaymentAsync(string email, int orderId, HttpContext httpContext);
        Task<MethodResult<string>> ProcessResponseAsync(VnPaymentResponseModel response);
        string GetRedirectUrl();
    }
}
