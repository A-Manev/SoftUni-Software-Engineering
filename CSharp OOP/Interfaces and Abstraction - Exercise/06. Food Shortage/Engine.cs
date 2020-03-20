using System;
using System.Linq;
using System.Collections.Generic;

using FoodShortage.Interfaces;
using FoodShortage.Models;

namespace FoodShortage
{
    public class Engine
    {
        private List<IBuyer> buyers;

        public Engine()
        {
            this.buyers = new List<IBuyer>();
        }

        public void Run()
        {
            int numberOfPeople = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfPeople; i++)
            {
                string[] commandArguments = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (commandArguments.Length == 3)
                {
                    this.AddRebel(commandArguments);
                }
                else if (commandArguments.Length == 4)
                {
                    this.AddCitizen(commandArguments);
                }
            }

            string command = Console.ReadLine();

            while (command != "End")
            {
                string name = command;

                var buyer = this.buyers.FirstOrDefault(x => x.Name == name);

                if (buyer != null)
                {
                    buyer.BuyFood();
                }

                command = Console.ReadLine();
            }

            this.PrintTotalBoughtFood();
        }

        private void AddRebel(string[] commandArguments)
        {
            string name = commandArguments[0];
            int age = int.Parse(commandArguments[1]);
            string group = commandArguments[2];

            Rebel rebel = new Rebel(name, age, group);

            this.buyers.Add(rebel);
        }

        private void AddCitizen(string[] commandArguments)
        {
            string name = commandArguments[0];
            int age = int.Parse(commandArguments[1]);
            string id = commandArguments[2];
            string birthdate = commandArguments[3];

            Citizen citizen = new Citizen(name, age, id, birthdate);

            this.buyers.Add(citizen);
        }

        private void PrintTotalBoughtFood()
        {
            Console.WriteLine(buyers.Sum(x => x.Food));
        }
    }
}
