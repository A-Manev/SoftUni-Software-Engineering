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

                Person person = new Person(firstName, lastName, age);

                people.Add(person);
            }

            PrintPeople(people);
        }

        private static void PrintPeople(List<Person> people)
        {
            foreach (var person in people.OrderBy(p => p.FirstName).ThenBy(p => p.Age))
            {
                Console.WriteLine(person);
            }
        }
    }
}
