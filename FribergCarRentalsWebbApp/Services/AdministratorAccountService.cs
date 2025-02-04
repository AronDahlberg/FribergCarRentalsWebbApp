using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class AdministratorAccountService(IAdministrator administratorRepository) : IAdministratorAccountService
    {
        private readonly IAdministrator _administratorRepository = administratorRepository;
        public Administrator? AuthenticateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmAccountAsAdministrator(int id)
        {
            return _administratorRepository.GetById(id) != null;
        }
    }
}
