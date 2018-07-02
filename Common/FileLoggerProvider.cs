using Microsoft.Extensions.Logging;

namespace Common
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string path)
        {
            return new FileLogger(path);
        }

        public void Dispose()
        {
        }
    }
}
