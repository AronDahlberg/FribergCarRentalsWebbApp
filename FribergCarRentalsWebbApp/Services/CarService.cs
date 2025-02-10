using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;
using System.Security.Cryptography.Xml;

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

        public void EditCar(Car car, string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile)
        {
            ArgumentNullException.ThrowIfNull(car);

            Price currentPrice = GetCurrentCarPrice(car);

            car.Name = carName;
            car.Description = description;

            if (currentPrice.AllowedDailyMileage != allowedMileage
                || currentPrice.DailyPrice != dailyPrice
                || currentPrice.AdditionalMileagePricePerInterval != pricePerExtraMile)
            {
                Price newPrice = new()
                {
                    AllowedDailyMileage = allowedMileage,
                    DailyPrice = dailyPrice,
                    AdditionalMileagePricePerInterval = pricePerExtraMile,
                    AdditionalMileagePriceInterval = 1,
                    PriceCreationDate = DateTime.UtcNow,
                };

                car.Prices.Add(newPrice); 
            }
            
            _carRepository.Update(car);
            _carRepository.Save();
        }

        public void AddImage(Car car, string imageLink)
        {
            ArgumentNullException.ThrowIfNull(car);

            if (string.IsNullOrWhiteSpace(imageLink))
            {
                throw new ArgumentException("Image link cannot be empty.", nameof(imageLink));
            }

            Image image = new()
            {
                Link = imageLink,
                UploadDate = DateTime.UtcNow,
            };

            car.Images.Add(image);

            _carRepository.Update(car);
            _carRepository.Save();
        }

        public void Unlist(Car car)
        {
            ArgumentNullException.ThrowIfNull(car);

            car.Unlisted = true;

            _carRepository.Update(car);
            _carRepository.Save();
        }
    }
}
