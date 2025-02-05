using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public interface IBooking
    {
        Booking? Get(int id);
        Booking? Find(Expression<Func<Booking, bool>> predicate);
        void Add(Booking booking);
        void Update(Booking booking);
        void Save();
    }
}
