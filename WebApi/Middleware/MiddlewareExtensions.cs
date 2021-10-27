using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleExceptionMiddleware>();
        }
    }
}