using System.Text.Json;
using DailyPlanner.Domain.Exceptions;
using DailyPlanner.Application.Exceptions;
using DailyPlanner.Infrastructure.Exceptions;

namespace DailyPlanner.ApiHost.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = new
            {
                title = GetTitle(exception),
                statusCode = GetStatusCode(exception),
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            httpContext.Response.StatusCode = response.statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                EntityValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                DailyPlannerException appException => appException.Title,
                _ => "Server Error"
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is EntityValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}