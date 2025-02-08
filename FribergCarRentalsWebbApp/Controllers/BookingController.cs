using FribergCarRentalsWebbApp.Common;
using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class BookingController(IBookingService bookingService, ICarService carService, IAccountService accountService) : Controller
    {
        private readonly IBookingService _bookingService = bookingService;
        private readonly ICarService _carService = carService;
        private readonly IAccountService _accountService = accountService;

        public IActionResult Index()
        {
            var cars = _carService.GetAllCars();

            return View(cars);
        }

        public IActionResult Details(int id)
        {
            return View(_carService.GetById(id));
        }

        [HttpPost]
        public IActionResult Book(int carId, string totalPrice, string pickupDateTime, string dropoffDateTime)
        {
            Car car = _carService.GetById(carId);

            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Cannot create booking without available customer id"));
            Customer customer = _accountService.GetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id: {userId}");

            Booking booking = new()
            {
                BookingCreation = DateTime.UtcNow,
                PickupDateTime = DateTime.Parse(pickupDateTime),
                DropoffDateTime = DateTime.Parse(dropoffDateTime),
                Car = car,
                CustomerAccount = customer,
                Invalidated = false,
                Payments =
                [
                    new() {
                        PaymentAmount = totalPrice,
                        TypeOfPayment = PaymentType.Booking,
                        ConfirmationDate = DateTime.UtcNow
                    }
                ]
            };

            _bookingService.CreateBooking(booking);

            return RedirectToAction("BookingConfirmation", new { id = booking.Id });
        }

        [HttpGet]
        public IActionResult BookingConfirmation(int id)
        {
            Booking booking = _bookingService.GetById(id) ?? throw new KeyNotFoundException($"Could not find booking with id: {id}");
            
            return View(booking);
        }
    }
}
