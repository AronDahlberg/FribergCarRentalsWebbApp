using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Data
{
    public interface ICustomer
    {
        Customer? GetById(int id);
        Customer? GetByCredentials(string email, string password);
        void Add(Customer customer);
        void Update(Customer customer);
        IEnumerable<Customer> All();
        void Save();
    }
}
