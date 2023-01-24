namespace Grandmark
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Set Request Headers
            httpContext.Request.Headers.Add("Access-Control-Allow-Origin", httpContext.Request.Headers.Origin);
            httpContext.Request.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, DELETE");
            httpContext.Request.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Request.Headers.Add("Access-Control-Allow-Headers", "Accept, Content-Type, Content-Length, Accept-Encoding, X-CSRF-Token, Authorization");

            await _next(httpContext); // calling next middleware
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }
}
