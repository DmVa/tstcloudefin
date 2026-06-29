using FileLoader;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Vendor.API
{
    internal sealed class ApiExceptionHandler(
       IProblemDetailsService problemDetailsService,
        ILogger<ApiExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ApiException)
            {
                return false;
            }
            logger.LogError(exception, "Api exception occurred");
            ApiException apiException = (ApiException)exception;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new { message = $"ApiException {apiException.Message}. code {apiException.StatusCode} ", };
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
