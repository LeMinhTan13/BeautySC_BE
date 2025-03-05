using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using BeautySCProject.Data.Models.VoucherModel;

namespace BeautySCProject.Service.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        public VoucherService(IVoucherRepository voucherRepository,IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<Voucher>> CreateVoucherAsync(VoucherCreateRequestModel request)
        {
            var voucher = _mapper.Map<Voucher>(request);
            var checkCreate = await _voucherRepository.CreateVoucherAsync(voucher);
            if(!checkCreate)
            {
                return new MethodResult<Voucher>.Failure("Can't create voucher", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<Voucher>.Success(voucher);
        }

        public async Task<MethodResult<IEnumerable<VoucherViewModel>>> GetAllVoucherAsync()
        {
            var result = await _voucherRepository.GetAllVoucherAsync();

            return new MethodResult<IEnumerable<VoucherViewModel>>.Success(result);
        }
        public async Task<MethodResult<IEnumerable<VoucherViewModel>>> GetAllVoucherByCustomerIdAsync(int customerId)
        {
            var result = await _voucherRepository.GetAllVoucherByCustomerIdAsync(customerId);

            return new MethodResult<IEnumerable<VoucherViewModel>>.Success(result);
        }
        public async Task<Voucher> GetVoucherByIdAsync(int voucherId)
        {
            return await _voucherRepository.GetVoucherByIdAsync(voucherId);
        }

        public async Task<MethodResult<VoucherViewModel>> GetVoucherDetailByIdAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            var result = _mapper.Map<VoucherViewModel>(voucher);
            if (result == null) 
            {
                return new MethodResult<VoucherViewModel>.Failure("Can't found Voucher!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<VoucherViewModel>.Success(result);
        }

        public async Task<MethodResult<string>> SetStatusVoucherEqualFalse(Voucher voucher)
        {
            var result = await _voucherRepository.SetStatusVoucherEqualFalse(voucher);
            
            if (!result)
            {
                return new MethodResult<string>.Failure("Can't set status!", StatusCodes.Status404NotFound);
            }
            return new MethodResult<string>.Success("");
        }

        public async Task<MethodResult<string>> UpdateVoucherAsync(VoucherUpdateRequestModel request)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(request.VoucherId);
            if (voucher == null)
            {
                return new MethodResult<string>.Failure("Can't found voucher!", StatusCodes.Status404NotFound);
            }
            voucher.StartDate = request.StartDate;
            voucher.EndDate = request.EndDate;
            voucher.Description = request.Description;
            voucher.DiscountAmount = request.DiscountAmount;
            voucher.MinimumPurchase = request.MinimumPurchase;
            voucher.VoucherName = request.VoucherName;
            voucher.VoucherCode = request.VoucherCode;
            voucher.Status = request.Status;
            var result = await _voucherRepository.UpdateVoucherAsync(voucher);
            if (!result) {
                return new MethodResult<string>.Failure("Can't update voucher!", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Update successfully");
        }
    }
}
