using System;
using System.Linq;

using Logger.Factories;
using Logger.Core.Contracts;
using Logger.Models.Contracts;

namespace Logger.Core
{
    public class Engine : IEngine
    {
        private ILogger logger;
        private ErrorFactory errorFactory;

        private Engine()
        {
            this.errorFactory = new ErrorFactory();
        }

        public Engine(ILogger logger)
            : this()
        {
            this.logger = logger;
        }

        public void Run()
        {
            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] inputArguments = input
                    .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string level = inputArguments[0];
                string dateTime = inputArguments[1];
                string message = inputArguments[2];

                try
                {
                    IError error = this.errorFactory.ProduceError(dateTime, message, level);

                    this.logger.Log(error);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(this.logger);
        }
    }
}
