
using Application.Exceptions;

namespace FUCarRentingSystem.Utilities
{
    public static class Authenticate 
    {
        public static void HeaderAuthenticate(this HttpContext context, out int userId)
        {
            if (context.Request.Cookies.TryGetValue("Authentication", out var value) is false)
                throw new UnauthorizeExpcetion("User is not login");
            userId = value != null? int.Parse(value) : -1;
        }
    }
}
