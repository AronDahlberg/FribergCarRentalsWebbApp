using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface IAccount
    {
        Customer? Get(int id);
        Customer? Find(Expression<Func<Customer, bool>> predicate);
        Administrator? FindAdministrator(Expression<Func<Administrator, bool>> predicate);
        Administrator? GetAdministratorById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        IEnumerable<Customer> All();
        void Save();
    }
}
