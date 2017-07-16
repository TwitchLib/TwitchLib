using System;
using Serilog;

namespace TwitchLib.Logging.Providers.SeriLog
{
    public class SerilogFactory : AbstractLoggerFactory
    {
        private readonly Serilog.ILogger logger;

        /// <summary>
        /// Creates a new SerilogFactory using the logger provided by <see cref="Log.Logger"/>.
        /// </summary>
        public SerilogFactory()
        {
            logger = Log.Logger;
        }

        public SerilogFactory(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public override ILogger Create(string name)
        {
            return new SerilogLogger(
                logger.ForContext(Serilog.Core.Constants.SourceContextPropertyName, name),
                this);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please see Serilog's LoggerConfiguration.MinimumLevel.");
        }
    }
}