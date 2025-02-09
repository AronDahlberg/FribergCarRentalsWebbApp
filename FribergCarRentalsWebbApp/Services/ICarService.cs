using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICarService
    {
        IEnumerable<Car> EagerGetAllCars();
        Car? EagerGetById(int id);
        Price GetCurrentCarPrice(Car car);
    }
}
