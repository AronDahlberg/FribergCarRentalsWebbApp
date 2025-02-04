using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface ICustomer
    {
        Customer? GetById(int id);
        Customer? Find(Expression<Func<Customer, bool>> predicate);
        void Add(Customer customer);
        void Update(Customer customer);
        IEnumerable<Customer> All();
        void Save();
    }
}
