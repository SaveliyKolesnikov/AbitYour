using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace AbitYour.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new object();
        
        public FileLogger(string path)
        {
            _filePath = path;
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if ((int)logLevel < 3 || formatter is null) return;
            
            lock (_lock)
            {
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }
        
        

        public bool IsEnabled(LogLevel logLevel)
        {
            return (int)logLevel >= 3;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}