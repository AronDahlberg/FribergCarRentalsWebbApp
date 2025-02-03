using FribergCarRentalsWebbApp.Models;
using FribergCarRentalsWebbApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class CustomerAccountController(CustomerAccountService customerAccountService) : Controller
    {
        private readonly CustomerAccountService _customerAccountService = customerAccountService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Customer? user = _customerAccountService.AuthenticateAccount(email, password);
            if (user == null)
            {
                return Json(new { success = false, message = "Invalid credentials." });
            }

            CreateNonAdminAuthCookie(user);

            return Json(new { success = true, message = "Login successful." });
        }

        [HttpPost]
        public ActionResult Signup(string email, string password)
        {
            Customer? user = _customerAccountService.CreateAccount(email, password);
            if (user == null)
            {
                return Json(new { success = false, message = "Signup failed." });
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
