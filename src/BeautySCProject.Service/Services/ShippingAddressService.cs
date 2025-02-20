using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.ShippingAddressModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IShippingAddressRepository _shippingAddressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ShippingAddressService(IShippingAddressRepository shippingAddressRepository, IMapper mapper, IUnitOfWork uow)
        {
            _shippingAddressRepository = shippingAddressRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<MethodResult<string>> CreateOneAsync(int customerId, ShippingAdressCreateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var shipAdd = _mapper.Map<ShippingAddress>(request);

                var anyShipAdd = await _shippingAddressRepository.CheckExistAsync(customerId);
                if (!anyShipAdd)
                {
                    shipAdd.IsDefault = true;
                }
                else
                {
                    if (request.IsDefault == true)
                    {
                        var checkCanDef = await _shippingAddressRepository.CancelDefaultAsync(customerId);
                        if (!checkCanDef)
                        {
                            return new MethodResult<string>.Failure("Fail while cancel default", StatusCodes.Status500InternalServerError);
                        }
                    }
                }

                shipAdd.CustomerId = customerId;

                var checkCreShipAdd = await _shippingAddressRepository.CreateOneAsync(shipAdd);
                if (!checkCreShipAdd)
                {
                    return new MethodResult<string>.Failure("Fail while create shipping address", StatusCodes.Status500InternalServerError);
                }

                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Create shipping address succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> UpdateOneAsync(int customerId, ShippingAdressUpdateRequest request)
        {
            var shipAdd = await _shippingAddressRepository.GetOneByIdAsync(request.ShippingAddressId);
            if (shipAdd == null)
            {
                return new MethodResult<string>.Failure("Shipping address not found", StatusCodes.Status404NotFound);
            }
            if (shipAdd.CustomerId != customerId)
            {
                return new MethodResult<string>.Failure("you do not have this shipping address", StatusCodes.Status400BadRequest);
            }

            shipAdd.Address = request.Address;
            shipAdd.PhoneNumber = request.PhoneNumber;

            var checkUpShipAdd = await _shippingAddressRepository.UpdateOneAsync(shipAdd);
            if (!checkUpShipAdd)
            {
                return new MethodResult<string>.Failure("Fail while update shipping address", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Create shipping address succesfully");
        }

        public async Task<ShippingAddress> GetOneByIdAsync(int shippingAddressId)
        {
            return await _shippingAddressRepository.GetOneByIdAsync(shippingAddressId);
        }

        public async Task<MethodResult<IEnumerable<ShippingAddress>>> GetAllByCustomerIdAsync(int customerId)
        {
            var result = await _shippingAddressRepository.GetAllByCustomerIdAsync(customerId);
            return new MethodResult<IEnumerable<ShippingAddress>>.Success(result);
        }

        public async Task<MethodResult<string>> DefaultAsync(int customerId, int shippingAddressId)
        {
            var shipAdd = await _shippingAddressRepository.GetOneByIdAsync(shippingAddressId);
            if (shipAdd == null)
            {
                return new MethodResult<string>.Failure("Shipping address not found", StatusCodes.Status404NotFound);
            }
            if (shipAdd.CustomerId != customerId)
            {
                return new MethodResult<string>.Failure("you do not have this shipping address", StatusCodes.Status400BadRequest);
            }

            var checkCanDef = await _shippingAddressRepository.CancelDefaultAsync(customerId);
            if (!checkCanDef)
            {
                return new MethodResult<string>.Failure("Fail while cancel default", StatusCodes.Status500InternalServerError);
            }

            shipAdd.IsDefault = true;
            var checkUpShipAdd = await _shippingAddressRepository.UpdateOneAsync(shipAdd);
            if (!checkUpShipAdd)
            {
                return new MethodResult<string>.Failure("Fail while update shipping address", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Default shipping address succesfully");
        }

        public async Task<MethodResult<string>> DeleteOneAsync(int customerId, int shippingAddressId)
        {
            var shipAdd = await _shippingAddressRepository.GetOneByIdAsync(shippingAddressId);
            if (shipAdd == null)
            {
                return new MethodResult<string>.Failure("Shipping address not found", StatusCodes.Status404NotFound);
            }
            if (shipAdd.CustomerId != customerId)
            {
                return new MethodResult<string>.Failure("you do not have this shipping address", StatusCodes.Status400BadRequest);
            }

            var checkDelShipAdd = await _shippingAddressRepository.DeleteOneAsync(shipAdd);
            if (!checkDelShipAdd)
            {
                return new MethodResult<string>.Failure("Fail while delete shipping address", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Delete shipping address succesfully");
        }
    }
}
