using System.Diagnostics;
using System.Net;
using Application.Responses;

namespace WebAPI.Middlewares;

public class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("Request: {Method} {Path} at {Time}", context.Request.Method, context.Request.Path, DateTime.Now);

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Method} {Path}", context.Request.Method, context.Request.Path);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal server error."));
        }

        stopwatch.Stop();
        _logger.LogInformation("Response: {StatusCode} in {Elapsed} ms", context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}