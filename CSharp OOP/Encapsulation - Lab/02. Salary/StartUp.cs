using System;
using System.Linq;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class StartUp
    {
        public static void Main()
        {
            int lines = int.Parse(Console.ReadLine());

            List<Person> people = new List<Person>();

            for (int i = 0; i < lines; i++)
            {
                string[] commandArguments = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string firstName = commandArguments[0];
                string lastName = commandArguments[1];
                int age = int.Parse(commandArguments[2]);
                decimal salary = decimal.Parse(commandArguments[3]);

                Person person = new Person(firstName, lastName, age, salary);

                people.Add(person);
            }

            decimal percentage = decimal.Parse(Console.ReadLine());

            IncreasePeopleSalary(people, percentage);

            PrintPeople(people);
        }

        private static void IncreasePeopleSalary(List<Person> people, decimal percentage)
        {
            foreach (var person in people)
            {
                person.IncreaseSalary(percentage);
            }
        }

        private static void PrintPeople(List<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
    }
}
