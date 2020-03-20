using System;
using System.Linq;

using ExplicitInterfaces.Interfaces;
using ExplicitInterfaces.Models;

namespace ExplicitInterfaces
{
    class Engine
    {
        public Engine()
        {

        }

        public void Run()
        {
            string command = Console.ReadLine();

            while (command != "End")
            {

                string[] commandArguments = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                this.PrintOutput(commandArguments);

                command = Console.ReadLine();
            }
        }

        private void PrintOutput(string[] commandArguments)
        {
            string name = commandArguments[0];
            string country = commandArguments[1];
            int age = int.Parse(commandArguments[2]);

            Citizen citizen = new Citizen(name, age, country);

            Type type = citizen.GetType();

            IResident iResidentCitizen = (IResident)Convert.ChangeType(citizen, type);

            Console.WriteLine(citizen.GetName());
            Console.WriteLine(iResidentCitizen.GetName());
        }
    }
}
