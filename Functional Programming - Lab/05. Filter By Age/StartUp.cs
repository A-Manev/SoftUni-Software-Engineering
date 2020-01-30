using System;
using System.Linq;
using System.Collections.Generic;

namespace FilterByAge
{
    class StartUp
    {
        static void Main()
        {
            List<Person> people = new List<Person>();

            int lines = int.Parse(Console.ReadLine());

            ReadPeople(people, lines);

            string condition = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string outputFormat = Console.ReadLine();

            Func<int, bool> filter = Filter(condition, age);
            Action<Person> writer = Writer(outputFormat);

            foreach (var person in people)
            {
                if (filter(person.Age))
                {
                    writer(person);
                }
            }
        }

        static Func<int, bool> Filter(string condition, int age)
        {
            if (condition == "younger")
            {
                return x => x < age;
            }

            return x => x >= age;
        }

        static Action<Person> Writer(string outputFormat)
        {
            if (outputFormat == "name")
            {
                return x => Console.WriteLine(x.Name);
            }
            else if (outputFormat == "age")
            {
                return x => Console.WriteLine(x.Age);
            }

            return x => Console.WriteLine($"{x.Name} - {x.Age}");
        }

        private static void ReadPeople(List<Person> people, int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                string[] input = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string name = input[0];
                int age = int.Parse(input[1]);

                Person person = new Person(name, age);
                people.Add(person);
            }
        }

        public class Person
        {
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
