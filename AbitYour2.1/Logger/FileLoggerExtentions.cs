using Microsoft.Extensions.Logging;

namespace AbitYour.Logger
{
    public static class FileLoggerExtentions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
            string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }

    }
}