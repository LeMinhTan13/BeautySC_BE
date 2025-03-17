using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Data.ViewModels;

namespace BeautySCProject.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Account> CreateAccountAsync(SignupRequest request);
        Task<Customer?> CreateCustomerAsync(Customer customer);
        Task<Customer?> FindByEmailAsync(string email);
        Task<Customer?> FindByPhoneNumberAsync(string number);
        Task<bool> UpdateCustomerAsync(Customer request);
        bool IsEmailTaken(string email);
		Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> UpdateProfileAsync(Customer user, ProfileUpdateRequest request);
        Task<Account> GetAccountByEmailAsync(string email);
        Task<int> GetNumberCustomerAsync();
        Task<IEnumerable<CustomerViewModel>> GetCustomersAsync();
    }
}
