using Microsoft.AspNetCore.Diagnostics;
using Vendor.Logic;

namespace Vendor.API
{
    internal sealed class VendorApiExceptionHandler(
        ILogger<VendorApiExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not VendorApiException)
            {
                return false;
            }
            logger.LogError(exception, "Api exception occurred");
            VendorApiException apiException = (VendorApiException)exception;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new { message = $"{apiException.Message}"};
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
