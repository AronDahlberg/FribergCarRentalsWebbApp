using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class AdminController(IAccountService accountService) : Controller
    {
        private readonly IAccountService _accountService = accountService;

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
    }
}
