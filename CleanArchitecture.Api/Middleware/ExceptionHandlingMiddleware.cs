using System.Net;
using System.Text.Json;

namespace CleanArchitecture.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        switch (exception)
        {
            case ApplicationException ex:
                {
                    if (ex.Message.ToLower().Contains("invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }
            case KeyNotFoundException ex:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        _logger.LogError(exception.Message);
        const string customErrorMessage = "Internal Server errors. Check Logs!";
        var result = JsonSerializer.Serialize(string.IsNullOrEmpty(exception.Message) ? customErrorMessage : exception.Message);
        await context.Response.WriteAsync(result);
    }
}