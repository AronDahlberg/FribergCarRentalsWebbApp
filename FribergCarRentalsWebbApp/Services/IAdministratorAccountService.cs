using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IAdministratorAccountService
    {
        Administrator? AuthenticateAccount(string email, string password);
        bool ConfirmAccountAsAdministrator(int id);
    }
}
