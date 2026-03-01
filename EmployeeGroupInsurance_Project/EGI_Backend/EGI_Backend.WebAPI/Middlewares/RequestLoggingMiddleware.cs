using System.Diagnostics;

namespace EGI_Backend.WebAPI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            // Log the incoming request
            _logger.LogInformation("HTTP Request: {Method} {Path} started", request.Method, request.Path);

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                var response = context.Response;

                // Log the outgoing response with performance time
                _logger.LogInformation("HTTP Response: {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms", 
                    request.Method, request.Path, response.StatusCode, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
