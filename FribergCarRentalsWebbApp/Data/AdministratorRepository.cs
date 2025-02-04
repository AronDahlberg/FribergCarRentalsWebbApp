using FribergCarRentalsWebbApp.Models;
using System;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class AdministratorRepository(ApplicationDbContext context) : IAdministrator
    {
        private readonly ApplicationDbContext _context = context;

        public Administrator? Find(Expression<Func<Administrator, bool>> predicate)
        {
            return _context.Administrators.FirstOrDefault(predicate);

        }
        public Administrator? GetById(int id)
        {
            return _context.Administrators.FirstOrDefault(a => a.Id == id);
        }

    }
}
