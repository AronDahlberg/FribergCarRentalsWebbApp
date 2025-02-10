using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICarService
    {
        IEnumerable<Car> EagerGetAllCars();
        Car? EagerGetById(int id);
        Price GetCurrentCarPrice(Car car);
        void AddCar(string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile, List<string> imageLinks);
    }
}
