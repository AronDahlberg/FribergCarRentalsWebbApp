using FribergCarRentalsWebbApp.Common;
using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class CustomerAccountService(IAccount accountRepository) : ICustomerAccountService
    {
        private readonly IAccount _accountRepository = accountRepository;

        public Customer? AuthenticateAccount(string email, string password)
        {
            return _accountRepository.Find(c => c.Email == email && c.Password == password);
        }

        public Customer? CreateAccount(string email, string password)
        {
            Customer customer = new()
            {
                Email = email,
                Password = password,
                CreationDate = DateTime.UtcNow,
                Deleted = false,
            };

            _accountRepository.Add(customer);
            _accountRepository.Save();

            return customer;
        }

        public bool EmailExists(string email)
        {
            return _accountRepository.Find(c => c.Email == email) != null;
        }

        public string? GetEmail(int id)
        {
            return _accountRepository.GetById(id)?.Email;
        }
        public bool ConfirmAccountAsCustomer(int id)
        {
            return _accountRepository.GetById(id) != null;
        }
    }
}
