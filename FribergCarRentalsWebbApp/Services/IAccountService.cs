using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IAccountService
    {
        Customer? AuthenticateCustomerAccount(string email, string password);
        Administrator? AuthenticateAdministratorAccount(string email, string password);
        Customer? CreateCustomerAccount(string email, string password);
        bool CustomerEmailExists(string email);
        string? GetCustomerEmail(int id);
        bool ConfirmAccountAsCustomer(int id);
        bool ConfirmAccountAsAdministrator(int id);
    }
}
