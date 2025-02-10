using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class AuthCookieService(IHttpContextAccessor httpContextAccessor) : IAuthCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public void CreateCustomerAuthCookie(Customer customer)
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Unexpected error: IHttpContextAccesor.HttpContext cannot be null.");
            }

            // Create a cookie with format: "UserId|IsAdmin"
            bool isAdmin = false;
            string cookieValue = $"{customer.Id}|{isAdmin.ToString().ToLower()}";
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps
                // Session cookie: no set expiration.
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserAuth", cookieValue, cookieOptions);
        }

        public void CreateAdministratorAuthCookie(Administrator administrator)
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Unexpected error: IHttpContextAccesor.HttpContext cannot be null.");
            }

            // Create a cookie with format: "UserId|IsAdmin"
            bool isAdmin = true;
            string cookieValue = $"{administrator.Id}|{isAdmin.ToString().ToLower()}";
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps
                // Session cookie: no set expiration.
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserAuth", cookieValue, cookieOptions);
        }

        public void DeleteUserAuthCookie()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Unexpected error: IHttpContextAccesor.HttpContext cannot be null.");
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserAuth");
        }
    }
}
