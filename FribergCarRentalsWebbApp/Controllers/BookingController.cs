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
            var cars = _carService.EagerGetAllCars().Where(c => !c.Unlisted);

            return View(cars);
        }

        public IActionResult Details(int id)
        {
            Car car = _carService.EagerGetById(id) ?? throw new KeyNotFoundException($"Could not find car with id: {id}");

            if (car.Unlisted)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult CreateListing()
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpGet]
        public IActionResult BookingConfirmation(int id)
        {
            Booking booking = _bookingService.EagerGetById(id) ?? throw new KeyNotFoundException($"Could not find booking with id: {id}");

            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Cannot create booking without available customer id"));

            if (userId != booking.CustomerAccount.Id)
            {
                return Unauthorized();
            }

            return View(booking);
        }

        [HttpPost]
        public IActionResult CreateListing(string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile, List<string> imageLinks)
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            _carService.AddCar(carName, description, dailyPrice, allowedMileage, pricePerExtraMile, imageLinks);

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Booking"), message = "Successfully created listing." });
        }

        [HttpPost]
        public IActionResult EditCarInfo(int carId, string carName, string description, int dailyPrice, int allowedMileage, int pricePerExtraMile)
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            Car car = _carService.EagerGetById(carId) ?? throw new KeyNotFoundException($"Could not find car with id: {carId}");

            _carService.EditCar(car, carName, description, dailyPrice, allowedMileage, pricePerExtraMile);

            return Json(new { success = true, message = "Successfully changed car info." });
        }

        [HttpPost]
        public IActionResult AddCarImage(int carId, string imageLink)
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            Car car = _carService.EagerGetById(carId) ?? throw new KeyNotFoundException($"Could not find car with id: {carId}");

            _carService.AddImage(car, imageLink);

            return Json(new { success = true, message = "Successfully added car image." });
        }

        [HttpPost]
        public IActionResult UnlistCar(int carId)
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            Car car = _carService.EagerGetById(carId) ?? throw new KeyNotFoundException($"Could not find car with id: {carId}");

            _carService.Unlist(car);

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Booking"), message = "Successfully unlisted car." });
        }

        [HttpPost]
        public IActionResult Book(int carId, string totalPrice, string pickupDateTime, string dropoffDateTime)
        {
            Car car = _carService.EagerGetById(carId) ?? throw new KeyNotFoundException($"Could not find car with id: {carId}");

            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Cannot create booking without available customer id"));
            Customer customer = _accountService.LazyGetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id: {userId}");

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

        [HttpPost]
        public IActionResult CancelBooking(int id)
        {
            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Could not find user"));
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            Customer customer = _accountService.LazyGetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id: {userId}");

            Booking booking = _bookingService.EagerGetById(id) ?? throw new KeyNotFoundException($"Could not find booking with id: {id}");

            if (customer != booking.CustomerAccount && !userIsAdmin)
            {
                return Unauthorized();
            }

            _bookingService.CancelBooking(booking);

            return Json(new { success = true, message = "Successfully cancelled booking." });
        }
    }
}
