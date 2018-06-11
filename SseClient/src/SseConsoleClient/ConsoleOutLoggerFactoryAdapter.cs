using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace SseConsoleClient
{
    public class ConsoleOutLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {

        public ConsoleOutLoggerFactoryAdapter()
            : base(null)
        {

        }

        public ConsoleOutLoggerFactoryAdapter(NameValueCollection properties)
            : base(properties)
        {

        }

        protected override ILog CreateLogger(string name, Common.Logging.LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            return new ConsoleOutLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
        }
    }
}