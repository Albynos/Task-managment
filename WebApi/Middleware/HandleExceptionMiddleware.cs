using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middleware
{
    public class HandleExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var (code, result) = exception switch
            {
                ValidationException validationException => 
                    (HttpStatusCode.BadRequest, JsonSerializer.Serialize(validationException.ValidationResult)),
                //TODO replace it by custom exception
                NullReferenceException => (HttpStatusCode.NotFound, string.Empty),
                _ => (HttpStatusCode.InternalServerError, string.Empty)
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new {error = exception.Message});
            }

            return context.Response.WriteAsync(result);
        }
    }
}