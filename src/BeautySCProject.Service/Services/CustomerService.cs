using System.Security.Claims;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.CustomerModel;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BeautySCProject.Data.ViewModels;

namespace BeautySCProject.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenValidator _tokenValidator;
        private readonly IOrderService _orderService;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IEmailService emailService, ITokenValidator tokenValidator, IOrderService orderService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _tokenValidator = tokenValidator;
            _orderService = orderService;
        }

        public async Task<MethodResult<Customer>> CreateCustomerAsyc(SignupRequest request)
        {
            var createdAccount = await _customerRepository.CreateAccountAsync(request);
            if (createdAccount == null)
            {
                return new MethodResult<Customer>.Failure("Failed to create account", StatusCodes.Status500InternalServerError);
            }

            var customer = _mapper.Map<Customer>(request);
            customer.AccountId = createdAccount.AccountId;

            var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);
            if (createdCustomer == null)
            {
                return new MethodResult<Customer>.Failure("Failed to create customer", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<Customer>.Success(createdCustomer);
        }
        public async Task<MethodResult<string>> SignupAsync(SignupRequest request)
        {
            var dupeEmailCustomer = await _customerRepository.FindByEmailAsync(request.Email);
            if (dupeEmailCustomer != null)
            {
                return new MethodResult<string>.Failure("Email already in use", StatusCodes.Status400BadRequest);
            }
            var dupePhoneCustomer = await _customerRepository.FindByPhoneNumberAsync(request.PhoneNumber);
            if (dupePhoneCustomer != null)
            {
                return new MethodResult<string>.Failure("Phone number already in use", StatusCodes.Status400BadRequest);
            }

            return await CreateCustomerAsyc(request).Result.Bind(customer => _emailService.SendAccountVerificationEmailAsync(customer));            
        }

        public async Task<MethodResult<Customer>> GetByEmailAsync(string email)
        {
            var user = await _customerRepository.FindByEmailAsync(email);       
            return new MethodResult<Customer>.Success(user);
        }
        public async Task<MethodResult<Customer>> GetByPhoneNumberAsync(string number)
        {
            var user = await _customerRepository.FindByPhoneNumberAsync(number);
            if (user != null)
            {
                return new MethodResult<Customer>.Success(user);
            }
            return new MethodResult<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound);
        }

        public async Task<MethodResult<string>> RequestProfileUpdateAsync(string email, ProfileUpdateRequest request)
            => await GetByEmailAsync(email).Result.Bind(user => UpdateProfileAsync(user, request));
        public async Task<MethodResult<string>> UpdateProfileAsync(Customer user, ProfileUpdateRequest request)
        {
            var result = await _customerRepository.UpdateProfileAsync(user, request);
            if (result)
            {
                return new MethodResult<string>.Success("Profile updated successfully");
            }
            return new MethodResult<string>.Failure("Failed to update profile. Please try again", StatusCodes.Status500InternalServerError);
        }
        public async Task<MethodResult<string>> RequestPasswordUpdateAsync(string email, PasswordUpdateRequest request)
            => await GetByEmailAsync(email).Result.Bind(async user =>
            {
                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Account.Password);
                if (!isPasswordCorrect)
                {
                    return new MethodResult<string>.Failure("Invalid password", StatusCodes.Status400BadRequest);
                }
                return await UpdatePasswordAsync(user, request.NewPassword);
            });
        public async Task<MethodResult<string>> UpdatePasswordAsync(Customer user, string newPassword)
        {
            user.Account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            var result = await _customerRepository.UpdateCustomerAsync(user);
            if (result)
            {
                return new MethodResult<string>.Success("Password updated successfully");
            }
            return new MethodResult<string>.Failure("Failed to update password. Please try again", StatusCodes.Status500InternalServerError);
        }
        public async Task<MethodResult<string>> RequestEmailUpdateAsync(string newEmail, string oldEmail)
        {
            var isEmailTaken = _customerRepository.IsEmailTaken(newEmail);
            if (isEmailTaken)
            {
                return new MethodResult<string>.Failure("Email already in use", StatusCodes.Status400BadRequest);
            }
            return await GetByEmailAsync(oldEmail).Result.Bind(user => _emailService.SendEmailUpdateVerificationAsync(user, newEmail));
        }
        public async Task<MethodResult<string>> UpdateEmailAsync(Customer user, string newEmail)
        {
            user.Account.Email = newEmail;
            var result = await _customerRepository.UpdateCustomerAsync(user);
            if (result)
            {
                return new MethodResult<string>.Success("Email updated successfully");
            }
            return new MethodResult<string>.Failure("Failed to update email. Please try again", StatusCodes.Status500InternalServerError);
        }
        public async Task<MethodResult<string>> RequestPasswordResetAsync(string email)
            => await GetByEmailAsync(email).Result.Bind(user => _emailService.SendPasswordResetEmailAsync(user));
        public async Task<MethodResult<string>> VerifyCreatedEmailAsync(Customer user)
        {
            if (user.ConfirmedEmail == true)
            {
                return new MethodResult<string>.Failure("Email already verified", StatusCodes.Status400BadRequest);
            }
            user.ConfirmedEmail = true;
            var result = await _customerRepository.UpdateCustomerAsync(user);
            if (result)
            {
                return new MethodResult<string>.Success("Email verified successfully");
            }
            else
            {
                return new MethodResult<string>.Failure("Failed to verify email. Please try again", StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<MethodResult<string>> VerifyAccountAsync(string token) =>
            await _tokenValidator.ValidateEmailVerificationToken(token)
            .Bind(email => GetByEmailAsync(email).Result
            .Bind(user => VerifyCreatedEmailAsync(user)));
        public async Task<MethodResult<string>> VerifyEmailUpdateAsync(string token) =>
           await _tokenValidator.ValidateEmailUpdateToken(token)
           .Bind(async emailTuple =>
           {
               var (oldEmail, newEmail) = emailTuple;
               var user = await _customerRepository.FindByEmailAsync(oldEmail);
               if (user == null) return new MethodResult<string>.Failure("Invalid token. Customer not found", StatusCodes.Status400BadRequest);
               var isEmailTaken = _customerRepository.IsEmailTaken(newEmail);
               if (isEmailTaken) return new MethodResult<string>.Failure("New email already taken", StatusCodes.Status400BadRequest);
               return await UpdateEmailAsync(user, newEmail);
           });
        public async Task<MethodResult<string>> VerifyPasswordResetAsync(string token, PasswordResetRequest request) =>
            await _tokenValidator.ValidatePasswordResetToken(token)
            .Bind(email => GetByEmailAsync(email).Result
            .Bind(user => UpdatePasswordAsync(user, request.NewPassword)));
        public async Task<MethodResult<string>> RequestResendVerificationEmailAsync(string email)
            => await GetByEmailAsync(email).Result.Bind(async user =>   
            {
                if (user.ConfirmedEmail == true)
                {
                    return new MethodResult<string>.Failure("Email already verified", StatusCodes.Status400BadRequest);
                }
                return await _emailService.ResendVerificationEmailAsync(user);
            });

        public async Task<MethodResult<Customer>> GetCustomerByIdAsync(int id)
        {
            var user = await _customerRepository.GetCustomerByIdAsync(id);
            if (user != null)
            {
                return new MethodResult<Customer>.Success(user);
            }
            return new MethodResult<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound);
        }

        public async Task<MethodResult<Account>> GetAccountByEmailAsync(string email)
        {
            var user = await _customerRepository.GetAccountByEmailAsync(email);
            if (user != null)
            {
                return new MethodResult<Account>.Success(user);
            }
            return new MethodResult<Account>.Failure("Customer not found", StatusCodes.Status404NotFound);
        }

        public async Task<MethodResult<ProfileViewModel>> GetProfileAsync(string email)
            => await GetByEmailAsync(email).Result.Bind<ProfileViewModel>(async customer =>
            {
                var result = _mapper.Map<ProfileViewModel>(customer);
                return new MethodResult<ProfileViewModel>.Success(result);
            });

        public async Task<MethodResult<string>> UpdateAvatarAsync(string email, string image)
            => await GetByEmailAsync(email).Result.Bind<string>(async customer =>
            {
                customer.Image = image;
                var check = await _customerRepository.UpdateCustomerAsync(customer);
                if (!check)
                {
                    return new MethodResult<string>.Failure("Fail while update customer", StatusCodes.Status500InternalServerError);
                }
                return new MethodResult<string>.Success("Update avatar successfully");
            });
    }
}
