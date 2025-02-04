using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class CustomerRepository(ApplicationDbContext context) : ICustomer
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

        public Customer? GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? Find(Expression<Func<Customer, bool>> predicate)
        {
            return _context.Customers.FirstOrDefault(predicate);
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
