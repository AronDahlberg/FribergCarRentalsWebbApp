using FribergCarRentalsWebbApp.Services;

namespace FribergCarRentalsWebbApp.Middleware
{
    public class AuthorizationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var accountService = context.RequestServices.GetRequiredService<IAccountService>();

            var authCookie = context.Request.Cookies["UserAuth"];

            int userId = 0;
            bool userIsCustomer = false;
            bool userIsAdmin = false;

            if (!string.IsNullOrEmpty(authCookie))
            {
                var parts = authCookie.Split('|');

                if (parts.Length == 2 &&
                        int.TryParse(parts[0], out userId) &&
                        bool.TryParse(parts[1], out bool adminFlag))
                {
                    if (adminFlag)
                    {
                        userIsAdmin = accountService.ConfirmAccountAsAdministrator(userId);
                    } else
                    {
                        userIsCustomer = accountService.ConfirmAccountAsCustomer(userId);
                    }
                }
            }

            context.Items["UserId"] = userId;
            context.Items["UserCustomer"] = userIsCustomer;
            context.Items["UserAdmin"] = userIsAdmin;

            await _next(context);
        }
    }
}
