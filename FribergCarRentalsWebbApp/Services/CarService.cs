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

        public void AddCar(string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile, List<string> imageLinks)
        {
            List<Image> images = [];

            foreach (string link in imageLinks)
            {
                images.Add(new() { Link = link, UploadDate = DateTime.UtcNow });
            }

            Car car = new()
            {
                Name = carName,
                Description = description,
                Images = images,
                ListingCreationDate = DateTime.UtcNow,
                Prices =
                [
                    new()
                    {
                        DailyPrice = dailyPrice,
                        AllowedDailyMileage = allowedMileage,
                        AdditionalMileagePricePerInterval = pricePerExtraMile,
                        AdditionalMileagePriceInterval = 1,
                        PriceCreationDate = DateTime.UtcNow
                    }
                ]
            };

            _carRepository.Add(car);
            _carRepository.Save();
        }
    }
}
