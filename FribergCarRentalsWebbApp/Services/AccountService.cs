using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class AccountService(IAccount accountRepository) : IAccountService
    {
        private readonly IAccount _accountRepository = accountRepository;

        public Customer? AuthenticateCustomerAccount(string email, string password)
        {
            return _accountRepository.Find(c => c.Email == email && c.Password == password);
        }

        public Administrator? AuthenticateAdministratorAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Customer? CreateCustomerAccount(string email, string password)
        {
            Customer customer = new()
            {
                Email = email,
                Password = password,
                CreationDate = DateTime.UtcNow,
                Deleted = false,
                Bookings = []
            };

            _accountRepository.Add(customer);
            _accountRepository.Save();

            return customer;
        }

        public bool CustomerEmailExists(string email)
        {
            return _accountRepository.Find(c => c.Email == email) != null;
        }

        public string? GetCustomerEmail(int id)
        {
            return _accountRepository.Get(id)?.Email;
        }
        public bool ConfirmAccountAsCustomer(int id)
        {
            return _accountRepository.Get(id) != null;
        }
        public bool ConfirmAccountAsAdministrator(int id)
        {
            return _accountRepository.GetAdministratorById(id) != null;
        }

        public Customer? LazyGetCustomerById(int id)
        {
            return _accountRepository.Get(id);
        }

        public Customer? EagerGetCustomerById(int id)
        {
            return _accountRepository.EagerGet(id);
        }
    }
}
