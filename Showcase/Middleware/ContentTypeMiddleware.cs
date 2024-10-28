using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Showcase.Middleware
{
    public class ContentTypeMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentTypeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey("Content-Type"))
                {
                    context.Response.Headers.Add("Content-Type", "text/plain");
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
