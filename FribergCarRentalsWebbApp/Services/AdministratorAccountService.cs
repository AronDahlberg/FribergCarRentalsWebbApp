using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class AdministratorAccountService(IAccount accountRepository) : IAdministratorAccountService
    {
        private readonly IAccount _accountRepository = accountRepository;
        public Administrator? AuthenticateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmAccountAsAdministrator(int id)
        {
            return _accountRepository.GetAdministratorById(id) != null;
        }
    }
}
