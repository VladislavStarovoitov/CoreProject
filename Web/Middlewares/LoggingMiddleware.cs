using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger("logs/memoryLog.txt");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            await _next.Invoke(context);
            watch.Stop();
            logger.LogInformation($"Request time: {watch.ElapsedMilliseconds}ms.");
        }
    }
}
