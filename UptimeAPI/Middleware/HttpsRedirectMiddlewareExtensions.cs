

using Microsoft.AspNetCore.Builder;

namespace UptimeAPI.Middleware
{
    /// <summary>
    /// Extension class to provide the method to be called to invoke the provided middleware object
    /// </summary>
    public static class HttpsRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpsRedirect(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpsRedirectMiddleware>();
        }
    }
}