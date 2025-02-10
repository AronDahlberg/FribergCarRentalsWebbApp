using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IAuthCookieService
    {
        void CreateCustomerAuthCookie(Customer customer);
        void CreateAdministratorAuthCookie(Administrator administrator);
        void DeleteUserAuthCookie();
    }
}
