using System.Text;

using Logger.Models.Contracts;
using System.Collections.Generic;

namespace Logger.Models
{
    public class Logger : ILogger
    {
        private ICollection<IAppender> appenders;

        public Logger(ICollection<IAppender> appenders)
        {
            this.appenders = appenders;
        }

        public IReadOnlyCollection<IAppender> Appenders => (IReadOnlyCollection<IAppender>)this.appenders;

        public void Log(IError error)
        {
            foreach (IAppender appender in this.appenders)
            {

                if (appender.Level <= error.Level)
                {
                    appender.Append(error);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Logger info");

            foreach (IAppender appender in this.appenders)
            {
                stringBuilder.AppendLine(appender.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
