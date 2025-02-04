using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICustomerAccountService
    {
        Customer? AuthenticateAccount(string email, string password);

        Customer? CreateAccount(string email, string password);
    }
}
