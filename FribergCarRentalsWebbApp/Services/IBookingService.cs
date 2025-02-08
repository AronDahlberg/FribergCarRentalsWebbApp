using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IBookingService
    {
        Booking? GetById(int id);
        List<(DateTime, DateTime)> GetUnavailableDateRanges(int carId);
        void CreateBooking(Booking booking);
    }
}
