using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class BookingController(IBookingService bookingService) : Controller
    {
        private readonly IBookingService _bookingService = bookingService;

        public IActionResult Index()
        {
            var cars = _bookingService.GetAllCars();

            var carsWithPrices = cars.Select(car => new CarWithPriceViewModel
            {
                Car = car,
                Price = _bookingService.GetCurrentCarPrice(car)
            });

            return View(carsWithPrices);
        }
    }
}
