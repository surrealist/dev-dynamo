using DevDynamo.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace DevDynamo.Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log request here

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    string result;

                    context.Response.ContentType = "application/json";
                    if (ex is NotFoundException e)
                    {
                        result = JsonConvert.SerializeObject(
                            new ProblemDetails
                            {
                                Title = ex.Message
                            });
                        context.Response.StatusCode = e.HttpStatus;
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(new { error = ex.Message });
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }

                    await context.Response.WriteAsync(result);

                    // Don't rethrow the exception, handled here.
                }
                finally
                {
                    // Log response
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    _logger.Log(LogLevel.Information, $"Response: {text}");

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }
    }

}
