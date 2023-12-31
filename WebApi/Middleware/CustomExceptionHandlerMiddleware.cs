﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    //result = JsonSerializer.Serialize(validationException.Errors.Select(e => e.ErrorMessage));
                    break;

                    //case NotFoundException:
                    //    code = HttpStatusCode.NotFound;
                    //    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}