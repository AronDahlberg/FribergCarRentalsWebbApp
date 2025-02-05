using FribergCarRentalsWebbApp.Models;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class BookingRepository : IBooking
    {
        public void Add(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Booking? Find(Expression<Func<Booking, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Booking? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
