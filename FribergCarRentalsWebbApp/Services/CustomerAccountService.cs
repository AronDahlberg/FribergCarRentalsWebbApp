using FribergCarRentalsWebbApp.Common;
using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class CustomerAccountService(ICustomer customerRepository) : ICustomerAccountService
    {
        private readonly ICustomer _customerRepository = customerRepository;

        public Customer? AuthenticateAccount(string email, string password)
        {
            return _customerRepository.GetByCredentials(email, password);
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
    }
}
