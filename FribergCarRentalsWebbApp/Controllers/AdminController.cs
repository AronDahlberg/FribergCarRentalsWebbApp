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
            int userId = (int)(HttpContext.Items["UserId"] ?? 0);
            bool userIsAdmin = (bool)(HttpContext.Items["UserAdmin"] ?? false);

            // If logged in as either someone not marked an admin,
            // or marked as one but has an invalid admin account
            if (userId != 0 && (!_accountService.ConfirmAccountAsAdministrator(userId) || !userIsAdmin))
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
