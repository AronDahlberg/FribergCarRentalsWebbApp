using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class BookingController(IBookingService bookingService, ICarService carService) : Controller
    {
        private readonly IBookingService _bookingService = bookingService;
        private readonly ICarService _carService = carService;

        public IActionResult Index()
        {
            var cars = _carService.GetAllCars();

            return View(cars);
        }
    }
}
