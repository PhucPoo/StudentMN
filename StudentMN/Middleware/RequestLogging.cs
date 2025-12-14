//namespace StudentMN.Middleware
//{
//    public class RequestLogging
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<RequestLogging> _logger;
//        public RequestLogging(
//            RequestDelegate next,
//            ILogger<RequestLogging> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }
//        public async Task InvokeAsync(HttpContext context)
//        {
//            _logger.LogInformation(
//                "HTTP {Method} - {Path}",
//                context.Request.Method,
//                context.Request.Path
//            );

//            await _next(context);
//        }
//    }
//}
