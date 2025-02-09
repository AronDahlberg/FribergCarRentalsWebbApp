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
            int userId = (int)(HttpContext.Items["UserId"] ?? 0);

            bool userIsCustomer = (bool)(HttpContext.Items["UserCustomer"] ?? false);

            if (userIsCustomer)
            {
                Customer customer = _AccountService.EagerGetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id. {userId}");

                return View(customer);
            }
            else
            {
                throw new NotImplementedException();
            }
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

        [HttpPost]
        public ActionResult ChangeEmail(string email)
        {
            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Could not find user"));
            Customer user = _AccountService.LazyGetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id: {userId}");

            _AccountService.ChangeCustomerEmail(user, email);

            return Json(new { success = true, message = "Successfully changed email." });
        }

        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword)
        {
            int userId = (int)(HttpContext.Items["UserId"] ?? throw new InvalidOperationException("Could not find user"));
            Customer user = _AccountService.LazyGetCustomerById(userId) ?? throw new KeyNotFoundException($"Could not find customer with id: {userId}");

            if (user.Password != currentPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            _AccountService.ChangeCustomerPassword(user, newPassword);

            return Json(new { success = true, message = "Successfully changed password." });
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
