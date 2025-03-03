using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.Configurations;
using BeautySCProject.Data.Models.VnPayModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionService _transactionService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        private readonly VnPayConfig _config;
        private readonly ICustomerService _userService;
        private readonly IUnitOfWork _uow;

        public PaymentService(ITransactionService transactionService,
            IOptions<VnPayConfig> options,
            IOrderRepository orderRepository,
            IOrderService orderService,
            ICustomerService userService,
            IUnitOfWork uow)
        {
            _transactionService = transactionService;
            _orderRepository = orderRepository;
            _orderService = orderService;
            _config = options.Value;
            _userService = userService;
            _uow = uow;
        }
        public async Task<MethodResult<string>> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {
            var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _config.Version);
            vnpay.AddRequestData("vnp_Command", _config.Command);
            vnpay.AddRequestData("vnp_TmnCode", _config.TmnCode);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY là: 10000000                
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config.CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config.Locale);

            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán nạp tiền cho Order có ID {model.OrderId} với số tiền {model.Amount}");
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _config.ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", tick); // Mã tham chiếu của giao dịch tại hệ thống của merchant.Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY.Không được trùng lặp trong ngày    
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(_config.BaseUrl, _config.HashSecret);

            return await Task.FromResult(new MethodResult<string>.Success(paymentUrl));
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();

            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_OrderRefId = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_TransactionId = vnpay.GetResponseData("vnp_TransactionNo");       
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_SecureHash = collections.FirstOrDefault(x => x.Key == "vnp_SecureHash").Value;
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo").ToString();
            var vnp_Amount = vnpay.GetResponseData("vnp_Amount");

            string pattern = @"ID \s*(\d+)\s* với số tiền";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(vnp_OrderInfo);
            string vnp_OrderId = match.Groups[1].Value;

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config.HashSecret);

            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }

            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderRefId = vnp_OrderRefId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash.ToString(),
                VnPayResponseCode = vnp_ResponseCode.ToString(),
                Amount = double.Parse(vnp_Amount) / 100,
                OrderId = int.Parse(vnp_OrderId)
            };
        }

        public async Task<MethodResult<string>> ProcessResponseAsync(VnPaymentResponseModel response)
            => await _orderService.GetOrderByIdAsync(response.OrderId).Result.Bind(async order =>
            {
                if (response.VnPayResponseCode != "00")
                {
                    return new MethodResult<string>.Failure("Payment fail", 400);
                }

                order.Status = Constants.ORDER_STATUS_CONFIRMED;
                var check = await _orderService.UpdateOrderAsync(order);
                if (!check)
                {
                    return new MethodResult<string>.Failure("Internal Server Error. Updating order status failed", 500);
                }

                return await CreateTransactionAsync(order, response);
            });
        public async Task<MethodResult<string>> CreateTransactionAsync(Order order, VnPaymentResponseModel response)
        {
            var transaction = new Transaction
            {
                Amount = (decimal) order.TotalAmount,
                CreatedDate = DateTime.Now,
                OrderId = order.OrderId,
                Description = response.OrderDescription
            };
            var result = await _transactionService.CreateTransactionAsync(transaction);
            if (result)
            {
                return new MethodResult<string>.Success("Payment success. Transaction Created");
            }
            return new MethodResult<string>.Failure("Internal Server Error. Transaction creation failed", 500);
        }
        public async Task<MethodResult<string>> CreatePaymentAsync(string email, int orderId, HttpContext httpContext)
            => await _orderService.GetOrderByIdAsync(orderId).Result
            .Bind(order => _userService.GetByEmailAsync(email).Result
            .Bind(async user =>
            {
                await _uow.BeginTransactionAsync();
                try
                {
                    if (order.Status != Constants.ORDER_STATUS_PENDING)
                    {
                        return new MethodResult<string>.Failure($"Order Status is {order.Status}", 400);
                    }
                    if (user.CustomerId != order.CustomerId)
                    {
                        return new MethodResult<string>.Failure("Customer have not this order", 400);
                    }
                    var vnpayModel = new VnPaymentRequestModel
                    {
                        Amount = (float)order.TotalAmount,
                        OrderId = orderId,
                        CreatedDate = DateTime.Now,
                    };
                    await _uow.CommitTransactionAsync();
                    return await CreatePaymentUrl(httpContext, vnpayModel);
                }
                catch (Exception)
                {
                    await _uow.RollbackTransactionAsync();
                    return new MethodResult<string>.Failure("Something went wrong", 500);
                }                
            }
            ));

        public string GetRedirectUrl()
        {
            return _config.RedirectUrl;
        }

        public async Task<MethodResult<IEnumerable<PaymentMethodViewModel>>> GetAllPaymentMethodAsync()
        {
            var result = await _orderRepository.GetAllPaymentMethodAsync();
            return new MethodResult<IEnumerable<PaymentMethodViewModel>>.Success(result);
        }
    }
}
