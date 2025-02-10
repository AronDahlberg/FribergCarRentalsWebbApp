using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;
using System.Diagnostics.CodeAnalysis;

namespace FribergCarRentalsWebbApp.Services
{
    public class AccountService(IAccount accountRepository, IBookingService bookingService) : IAccountService
    {
        private readonly IAccount _accountRepository = accountRepository;
        private readonly IBookingService _bookingService = bookingService;

        public Customer? AuthenticateCustomerAccount(string email, string password)
        {
            return _accountRepository.Find(c => c.Email == email && c.Password == password);
        }

        public Administrator? AuthenticateAdministratorAccount(string email, string password)
        {
            return _accountRepository.FindAdministrator(a => a.Email == email && a.Password == password);
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

        public void ChangeCustomerEmail(Customer customer, string email)
        {
            ArgumentNullException.ThrowIfNull(customer);

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }

            customer.Email = email;

            _accountRepository.Update(customer);
            _accountRepository.Save();
        }

        public void ChangeCustomerPassword(Customer customer, string password)
        {
            ArgumentNullException.ThrowIfNull(customer);

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(password));
            }

            customer.Password = password;

            _accountRepository.Update(customer);
            _accountRepository.Save();
        }

        public void DeleteCustomerAccount(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            customer.Deleted = true;
            customer.DeletionDate = DateTime.UtcNow;
            customer.Email = null;
            customer.Password = null;

            if (customer.Bookings != null)
            {
                foreach (var booking in customer.Bookings)
                {
                    _bookingService.CancelBooking(booking);
                } 
            }

            _accountRepository.Update(customer);
            _accountRepository.Save();
        }

        public string? GetAdministratorEmail(int id)
        {
            return _accountRepository.GetAdministratorById(id)?.Email;
        }
    }
}
