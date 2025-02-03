using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Data
{
    public class AdministratorRepository(ApplicationDbContext context) : IAdministrator
    {
        private readonly ApplicationDbContext _context = context;

        public Administrator? GetByCredentials(string email, string password)
        {
            return _context.Administrators
                .FirstOrDefault(c => c.Email == email && c.Password == password);
        }
    }
}
