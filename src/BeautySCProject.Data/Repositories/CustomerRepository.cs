using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.CustomerModel;
using System.Linq.Expressions;



namespace BeautySCProject.Data.Repositories
{
	public class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly BeautyscDbContext _dbContext;
		private IMapper _mapper;

        public CustomerRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_unitOfWork = unitOfWork;
			_dbContext = dbContext;
			_mapper = mapper;
        }

		public async Task<bool> UpdateProfileAsync(Customer user, ProfileUpdateRequest request)
		{
			try
			{
				user.PhoneNumber = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : user.PhoneNumber;
				user.FullName = !string.IsNullOrEmpty(request.FullName) ? request.FullName : user.FullName;
				user.Birthday = request.Birthday.HasValue ? request.Birthday : user.Birthday;			
				return await UpdateCustomerAsync(user);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return await Task.FromResult(false);
			}
		}

		public async Task<Customer?> GetCustomerByIdAsync(int id)
		{
			return await Entities.Include(x => x.Account).FirstOrDefaultAsync(user => user.CustomerId == id);
		}

        public async Task<Account> CreateAccountAsync(SignupRequest request)
        {
            try
            {
                var account = _mapper.Map<Account>(request);
                await _dbContext.Accounts.AddAsync(account);
                await _unitOfWork.SaveChangesAsync();
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            try
            {	
                Entities.AddAsync(customer);               
                await _unitOfWork.SaveChangesAsync();
                return customer;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		public async Task<Customer?> FindByEmailAsync(string email)
		{
			return await Entities.Include(x => x.Account).FirstOrDefaultAsync(u => u.Account.Email == email);
		}
		public async Task<Customer?> FindByPhoneNumberAsync(string number)
		{
			return await Entities.Include(x => x.Account).FirstOrDefaultAsync(u => u.PhoneNumber == number);
		}
		public async Task<bool> UpdateCustomerAsync(Customer request)
		{
			var user = await Entities.FirstOrDefaultAsync(u => u.CustomerId == request.CustomerId);
			if (user == null) return await Task.FromResult(false);
			Entities.Entry(user).CurrentValues.SetValues(request);
			await _unitOfWork.SaveChangesAsync();
			return await Task.FromResult(true);
		}
		public bool IsEmailTaken(string email)
		{
			return _dbContext.Accounts.Any(u => u.Email == email);
		}

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await _dbContext.Accounts.Include(x => x.Customer).FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}
