using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IBookingService
    {
        Booking? EagerGetById(int id);
        IEnumerable<Booking> EagerAll();
        List<(DateTime, DateTime)> GetUnavailableDateRanges(int carId);
        void CreateBooking(Booking booking);
        void CancelBooking(Booking booking);
    }
}
