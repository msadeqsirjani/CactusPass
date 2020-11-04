using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAuthenticationToken(this HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            var token = string.Empty;

            if (authorizationHeader != null && string.IsNullOrEmpty(authorizationHeader) &&
                authorizationHeader.StartsWith("Bearer ")) return token;

            token = authorizationHeader?.Substring("Bearer ".Length);

            return token;
        }
    }
}
