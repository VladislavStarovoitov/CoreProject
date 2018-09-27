using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private object _lock = new object();

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory)
        {
            //ILogger logger = loggerFactory.CreateLogger("memoryLog.txt");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            await _next.Invoke(context);
            watch.Stop();
            LogTime(watch.ElapsedMilliseconds);
        }

        private void LogTime(long ms)
        {
            lock (_lock)
            {
                File.AppendAllText("logs/timeLog.txt", $"Request time: {ms}ms." + Environment.NewLine);
            }
        }
    }
}
