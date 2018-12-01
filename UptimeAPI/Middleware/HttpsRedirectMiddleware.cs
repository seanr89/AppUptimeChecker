
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UptimeAPI.Middleware
{
    /// <summary>
    /// C# Middle to provide the addition of HTTPS method
    /// </summary>
    public class HttpsRedirectMiddleware
    {
        private RequestDelegate _next;

        public HttpsRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.IsHttps)
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.Redirect(string.Format("https://{0}{1}", context.Request.Host.ToString(), context.Request.Path.ToString()), true);
            }
        }

    }
}