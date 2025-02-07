using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface IBookingService
    {
        IEnumerable<Car> GetAllCars();
        Price GetCurrentCarPrice(Car car);
    }
}
