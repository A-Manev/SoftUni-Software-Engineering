using System;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main()
        {
            string name = "Pesho";
            int age = 24;

            Person person = new Person()
            {
                Name = name,
                Age = age
            };

            Console.WriteLine($"{person.Name} -> {person.Age}");
        }
    }
}
