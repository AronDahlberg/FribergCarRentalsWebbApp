using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Data
{
    public interface IAdministrator
    {
        Administrator? GetByCredentials(string email, string password);
    }
}
