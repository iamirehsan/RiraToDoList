using RiraToDoList.Domain.Base;
using RiraToDoList.Domain.LogService.@interface;
using System.Net;
using System.Text;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        string message;


        switch (exception)
        {
            case ManagedException ex:
                _logger.WriteError("Managed exption.", exception);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = ex.ErrorMessage;
                break;
            default:
                _logger.WriteError("Unhandle exption.", exception);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = exception.Message;

                break;
        }
        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = message,

        };
        var jsonResponse = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(jsonResponse);


    }
}
