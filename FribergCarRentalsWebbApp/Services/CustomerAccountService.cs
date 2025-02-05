using FribergCarRentalsWebbApp.Common;
using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class CustomerAccountService(IAccount customerRepository) : ICustomerAccountService
    {
        private readonly IAccount _customerRepository = customerRepository;

        public Customer? AuthenticateAccount(string email, string password)
        {
            return _customerRepository.Find(c => c.Email == email && c.Password == password);
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

            _customerRepository.Add(customer);
            _customerRepository.Save();

            return customer;
        }

        public bool EmailExists(string email)
        {
            return _customerRepository.Find(c => c.Email == email) != null;
        }

        public string? GetEmail(int id)
        {
            return _customerRepository.GetById(id)?.Email;
        }
        public bool ConfirmAccountAsCustomer(int id)
        {
            return _customerRepository.GetById(id) != null;
        }
    }
}
