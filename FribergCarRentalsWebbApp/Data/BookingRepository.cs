using FribergCarRentalsWebbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class BookingRepository(ApplicationDbContext context) : IBooking
    {
        private readonly ApplicationDbContext _context = context;

        public void Add(Booking booking)
        {
            _context.Add(booking);
        }

        public IEnumerable<Booking> All()
        {
            return _context.Bookings.AsEnumerable();
        }

        public Booking? Find(Expression<Func<Booking, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Booking? Get(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
