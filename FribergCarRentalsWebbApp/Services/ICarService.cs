using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();
        Price GetCurrentCarPrice(Car car);
    }
}
