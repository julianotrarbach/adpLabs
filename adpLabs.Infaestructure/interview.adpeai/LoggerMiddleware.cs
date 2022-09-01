using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace AdpLabs.Infaestructure.interview.adpeai
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<LoggerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var task = Task.Run(() =>
            {
                var httpContext = context;
                var requestPath = $"{httpContext.Request.Method} {httpContext.Request.Host}{httpContext.Request.Path}";

                var message = new
                {
                    //ExceptionMessage = context.co.Exception?.Message ?? "",
                    Title = "Title",
                    Request = new
                    {
                        httpContext.Request.Method,
                        httpContext.Request.Host,
                        httpContext.Request.Path,
                        httpContext.Request.QueryString,
                        httpContext.Request.Headers
                    },
                    Response = new
                    {
                        context.Response.StatusCode,
                        context.Response.Headers
                    }
                };
                var logJson = JsonConvert.SerializeObject(message);
                _logger.LogInformation($"{requestPath} => {logJson}");
            });

            task.GetAwaiter().GetResult();
            //base.OnActionExecuted(context);
            await _next(context);
        }

    }
    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();
        }
    }
}


