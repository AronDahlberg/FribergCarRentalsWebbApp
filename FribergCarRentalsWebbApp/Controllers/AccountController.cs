using FribergCarRentalsWebbApp.Models;
using static FribergCarRentalsWebbApp.Common.AccountHelper;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentalsWebbApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Account? user = AuthenticateAccount(email, password);
            if (user == null)
            {
                return Json(new { success = false, message = "Invalid credentials." });
            }

            CreateAuthCookie(user);

            return Json(new { success = true, message = "Login successful." });
        }

        [HttpPost]
        public ActionResult Signup(string email, string password)
        {
            Account? user = CreateAccount(email, password);
            if (user == null)
            {
                return Json(new { success = false, message = "Signup failed." });
            }

            CreateAuthCookie(user);

            return Json(new { success = true, message = "Signup successful." });
        }

        private void CreateAuthCookie(Account user)
        {
            // Create a cookie with format: "UserId|IsAdmin"
            bool isAdmin = user is Administrator;
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
