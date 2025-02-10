using FribergCarRentalsWebbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class AccountRepository(ApplicationDbContext context) : IAccount
    {
        private readonly ApplicationDbContext _context = context;
        public void Add(Customer customer)
        {
            _context.Add(customer);
        }

        public IEnumerable<Customer> All()
        {
            return _context.Customers.AsEnumerable();
        }

        public IEnumerable<Customer> EagerAll()
        {
            return [.. _context.Customers
                    .Include(c => c.Bookings)!
                        .ThenInclude(b => b.Car)
                            .ThenInclude(c => c.Images)
                    .Include(c => c.Bookings)!
                        .ThenInclude(b => b.Car)
                            .ThenInclude(c => c.Prices)
                    .Include(c => c.Bookings)!
                        .ThenInclude(b => b.Payments)];
        }

        public Customer? Get(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? EagerGet(int id)
        {
            return _context.Customers
                    .Include(c => c.Bookings)!
                        .ThenInclude(b => b.Car)
                    .FirstOrDefault(c => c.Id == id);
        }

        public Customer? Find(Expression<Func<Customer, bool>> predicate)
        {
            return _context.Customers.FirstOrDefault(predicate);
        }
        public Administrator? FindAdministrator(Expression<Func<Administrator, bool>> predicate)
        {
            return _context.Administrators.FirstOrDefault(predicate);

        }
        public Administrator? GetAdministratorById(int id)
        {
            return _context.Administrators.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
