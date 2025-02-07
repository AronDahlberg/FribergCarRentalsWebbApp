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

            return View(cars);
        }
    }
}
