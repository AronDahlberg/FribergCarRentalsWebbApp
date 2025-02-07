using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();
        Car GetById(int id);
        Price GetCurrentCarPrice(Car car);
    }
}
