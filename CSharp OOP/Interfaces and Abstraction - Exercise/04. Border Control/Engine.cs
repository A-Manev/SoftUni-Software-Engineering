using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    class Engine
    {
        private List<IIdentifiable> populations;

        public Engine()
        {
            this.populations = new List<IIdentifiable>();
        }

        public void Run()
        {
            string command = Console.ReadLine();

            while (command != "End")
            {
                string[] commandArguments = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (commandArguments.Length == 3)
                {
                    this.AddCitizen(commandArguments);
                }
                else
                {
                    this.AddRobot(commandArguments);
                }

                command = Console.ReadLine();
            }

            PrintFakeIds();
        }

        private void AddCitizen( string[] commandArguments)
        {
            string name = commandArguments[0];
            int age = int.Parse(commandArguments[1]);
            string id = commandArguments[2];

            Citizen citizen = new Citizen(name, age, id);

            this.populations.Add(citizen);
        }

        private void AddRobot(string[] commandArguments)
        {
            string model = commandArguments[0];
            string id = commandArguments[1];

            Robot robot = new Robot(model, id);

            this.populations.Add(robot);
        }

        private void PrintFakeIds()
        {
            string fakeId = Console.ReadLine();

            List<string> allFakeIds = this.populations
                .Where(x => x.Id.EndsWith(fakeId))
                .Select(x => x.Id)
                .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, allFakeIds));
        }
    }
}
