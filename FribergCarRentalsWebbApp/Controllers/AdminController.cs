using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class AdminController(IAccountService accountService, IAuthCookieService authCookieService) : Controller
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IAuthCookieService _authCookieService = authCookieService;

        public IActionResult Index()
        {
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            if (!userIsAdmin)
            {
                return Unauthorized();
            }

            return View();
        }

        public IActionResult Customers()
        {
            return View();
        }

        public IActionResult Bookings()
        {
            return View();
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
