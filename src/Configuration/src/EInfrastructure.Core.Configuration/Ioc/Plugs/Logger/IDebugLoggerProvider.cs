namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public interface IDebugLoggerProvider
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug("Processing request from {Address}", address)</example>
        void LogDebug(System.Exception exception, string message, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug("Processing request from {Address}", address)</example>
        void LogDebug(string message, params object[] args);
    }
}