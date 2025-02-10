using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public interface ICarService
    {
        IEnumerable<Car> EagerGetAllCars();
        Car? EagerGetById(int id);
        Price GetCurrentCarPrice(Car car);
        void AddCar(string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile, List<string> imageLinks);
        void AddImage(Car car, string imageLink);
        void EditCar(Car car, string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile);
        void Unlist(Car car);
    }
}
