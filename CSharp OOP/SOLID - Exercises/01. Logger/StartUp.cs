using System;
using System.Linq;
using System.Collections.Generic;

using Logger.Factories;
using Logger.Models.Contracts;
using Logger.Core.Contracts;
using Logger.Core;

namespace Logger
{
    public class StartUp
    {
        public static void Main()
        {
            int appendersCount = int.Parse(Console.ReadLine());

            ICollection<IAppender> appenders = new List<IAppender>();

            ParseAppendersInput(appendersCount, appenders);

            ILogger logger = new Logger.Models.Logger(appenders);

            IEngine engine = new Engine(logger);

            engine.Run();
        }

        private static void ParseAppendersInput(int appendersCount, ICollection<IAppender> appenders)
        {
            AppenderFactory appenderFactory = new AppenderFactory();

            for (int i = 0; i < appendersCount; i++)
            {
                string[] appendersArguments = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string appenderType = appendersArguments[0];
                string layoutType = appendersArguments[1];
                string level = "INFO";

                if (appendersArguments.Length == 3)
                {
                    level = appendersArguments[2];
                }

                try
                {
                    IAppender appender = appenderFactory.ProduceAppender(appenderType, layoutType, level);

                    appenders.Add(appender);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            }
        }
    }
}
