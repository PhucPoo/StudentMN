using System.Net;
using System.Text.Json;

namespace StudentMN.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                success = false,
                message = "Lỗi hệ thống",
                detail = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        
    }
}
