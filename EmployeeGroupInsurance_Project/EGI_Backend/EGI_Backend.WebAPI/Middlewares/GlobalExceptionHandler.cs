using EGI_Backend.Application.Exceptions;
using EGI_Backend.WebAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EGI_Backend.WebAPI.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An internal server error occurred.",
                Detail = exception.Message,
                TraceId = httpContext.TraceIdentifier
            };

            // 🛠️ Map Custom Domain Exceptions
            if (exception is BaseException domainEx)
            {
                errorResponse.StatusCode = domainEx.StatusCode;
                errorResponse.Message = domainEx.Message;
            }
            // 🛠️ Map Common System Exceptions
            else if (exception is UnauthorizedAccessException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Unauthorized access.";
            }
            else if (exception is KeyNotFoundException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = "The requested resource was not found.";
            }
            else if (exception is JsonException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid JSON in the request body.";
            }

            httpContext.Response.StatusCode = errorResponse.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
