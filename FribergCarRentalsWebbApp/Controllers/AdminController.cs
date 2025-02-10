using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class AdminController(IAccountService accountService, IAuthCookieService authCookieService, IBookingService bookingService) : Controller
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IAuthCookieService _authCookieService = authCookieService;
        private readonly IBookingService _bookingService = bookingService;

        public IActionResult Index()
        {
            int userId = (int)(HttpContext.Items["UserId"] ?? 0);
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin && userId != 0)
            {
                return Unauthorized();
            }

            return View();
        }

        public IActionResult Customers()
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            var customers = _accountService.EagerAllCustomers().Where(c => !c.Deleted);

            return View(customers);
        }

        public IActionResult Bookings()
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            var bookings = _bookingService.EagerAll().Where(b => !b.Invalidated);

            return View(bookings);
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Administrator? admin = _accountService.AuthenticateAdministratorAccount(email, password);
            if (admin == null)
            {
                return Unauthorized("Invalid credentials");
            }

            _authCookieService.CreateAdministratorAuthCookie(admin);

            return Json(new { success = true, message = "Login successful." });
        }
    }
}
