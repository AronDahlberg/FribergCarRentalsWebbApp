using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    /// <summary>
    /// For customer accounts
    /// </summary>
    /// <param name="AccountService"></param>
    public class AccountController(IAccountService AccountService) : Controller
    {
        private readonly IAccountService _AccountService = AccountService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Customer? user = _AccountService.AuthenticateCustomerAccount(email, password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            CreateNonAdminAuthCookie(user);

            return Json(new { success = true, message = "Login successful." });
        }

        [HttpPost]
        public ActionResult Signup(string email, string password)
        {
            if (_AccountService.CustomerEmailExists(email))
            {
                return Conflict("Email is already in use");
            }

            Customer? user = _AccountService.CreateCustomerAccount(email, password);

            if (user == null)
            {
                return StatusCode(500, "Unkown error");
            }

            CreateNonAdminAuthCookie(user);

            return Json(new { success = true, message = "Signup successful." });
        }

        private void CreateNonAdminAuthCookie(Customer user)
        {
            // Create a cookie with format: "UserId|IsAdmin"
            bool isAdmin = false;
            string cookieValue = $"{user.Id}|{isAdmin.ToString().ToLower()}";
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Secure = Request.IsHttps
                // Session cookie: no set expiration.
            };
            Response.Cookies.Append("UserAuth", cookieValue, cookieOptions);
        }
    }
}
