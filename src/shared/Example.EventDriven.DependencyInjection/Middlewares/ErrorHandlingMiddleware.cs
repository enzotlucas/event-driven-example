using Example.EventDriven.Domain.Exceptions;
using Example.EventDriven.Domain.Gateways.Logger;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Example.EventDriven.DependencyInjection.Middlewares
{
    public sealed class ErrorHandlingMiddleware
    {
        private readonly ILoggerManager _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(ILoggerManager logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                _logger.Log("Business error caught by middleware", LoggerManagerSeverity.WARNING, ("exception", ex));

                await HandleExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                _logger.Log("Validation error caught by middleware", LoggerManagerSeverity.WARNING, ("exception", ex));

                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogException("Unexpected error caught by middleware", LoggerManagerSeverity.ERROR, ex);

                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            var code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { Errors = new string[1] { exception.Message } });

            return ErrorResponse(context, result, code);
        }

        private static Task HandleExceptionAsync(HttpContext context, ValidationException exception)
        {
            var code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { Errors = exception.Errors.Select(e => e.ErrorMessage).ToArray() });

            return ErrorResponse(context, result, code);
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            return ErrorResponse(context, code);
        }

        private static Task ErrorResponse(HttpContext context, HttpStatusCode code)
        {
            var result = JsonConvert.SerializeObject(new { Errors = new string[1] { "UnexpectedError" } });

            return ErrorResponse(context, result, code);
        }

        private static Task ErrorResponse(HttpContext context, string result, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
