using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Common
{
    public static class AccountHelper
    {
        /// <summary>
        /// Checks the current HTTP context and returns the user's authentication status.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>An AuthStatus enum value representing the user's auth level.</returns>
        public static AuthStatus GetAuthStatus(HttpContext context)
        {
            // If there is no HTTP context or no user ID stored, return unauthorized.
            if (context == null || context.Items["UserId"] == null)
            {
                return AuthStatus.UnAuthorized;
            }

            // Retrieve the admin flag if available.
            bool isAdmin = false;
            if (context.Items["IsAdmin"] is bool adminFlag)
            {
                isAdmin = adminFlag;
            }

            return isAdmin ? AuthStatus.AdminAuthorized : AuthStatus.Authorized;
        }

        public static Account AuthenticateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static Account CreateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
