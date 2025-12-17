using System.Net;
using System.Text.Json;

namespace StudentMN.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;
        public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response đã bắt đầu, không thể gửi lỗi JSON cho {Method} {Path}",
                    context.Request.Method, context.Request.Path);
                _logger.LogError(exception, "Exception xảy ra sau khi response đã bắt đầu");
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Lỗi hệ thống",
                detail = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
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
