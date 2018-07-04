using Microsoft.Extensions.Logging;

namespace Common
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string path)
        {
            return new FileLogger($"logs/{path}");
        }

        public void Dispose()
        {
        }
    }
}
