using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class CarService(ICar carRepository) : ICarService
    {
        private readonly ICar _carRepository = carRepository;

        public IEnumerable<Car> EagerGetAllCars()
        {
            return _carRepository.EagerAll();
        }

        public Car? EagerGetById(int id)
        {
            return _carRepository.EagerGet(id);
        }

        public Price GetCurrentCarPrice(Car car)
        {
            ArgumentNullException.ThrowIfNull(car);

            if (car.Prices == null || car.Prices.Count == 0)
                throw new ArgumentException("Prices not loaded or car has no prices");

            return car.Prices
              .OrderByDescending(p => p.PriceCreationDate)
              .FirstOrDefault() ?? throw new Exception("Unexpected error");
        }
    }
}
