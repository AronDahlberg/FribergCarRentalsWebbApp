using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IAccountService
    {
        Customer? LazyGetCustomerById(int id);
        Customer? EagerGetCustomerById(int id);
        Customer? AuthenticateCustomerAccount(string email, string password);
        Administrator? AuthenticateAdministratorAccount(string email, string password);
        Customer? CreateCustomerAccount(string email, string password);
        void DeleteCustomerAccount(Customer customer);
        bool CustomerEmailExists(string email);
        string? GetCustomerEmail(int id);
        string? GetAdministratorEmail(int id);
        void ChangeCustomerEmail(Customer customer, string email);
        void ChangeCustomerPassword(Customer customer, string password);
        bool ConfirmAccountAsCustomer(int id);
        bool ConfirmAccountAsAdministrator(int id);
        IEnumerable<Customer> EagerAllCustomers();
    }
}
