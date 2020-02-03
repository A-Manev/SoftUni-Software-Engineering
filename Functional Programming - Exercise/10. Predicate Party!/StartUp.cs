using System;
using System.Linq;
using System.Collections.Generic;

namespace PredicateParty_
{
    class StartUp
    {
        static void Main()
        {
            List<string> guests = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            string command = Console.ReadLine();

            while (command != "Party!")
            {
                string[] commandArguments = command
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

                string commandType = commandArguments[0];
                string differentCriteria = commandArguments[1];
                string @string = commandArguments[2];

                Predicate<string> predicate = GetPredicate(differentCriteria, @string);

                if (commandType == "Remove")
                {
                    guests.RemoveAll(predicate);
                }
                else if (commandType == "Double")
                {
                    DoubleGuests(guests, predicate);
                }

                command = Console.ReadLine();
            }

            if (guests.Any())
            {
                Console.WriteLine($"{string.Join(", ", guests)} are going to the party!");
            }
            else
            {
                Console.WriteLine("Nobody is going to the party!");
            }
        }

        private static void DoubleGuests(List<string> guests, Predicate<string> predicate)
        {
            for (int i = 0; i < guests.Count; i++)
            {
                string currentGuest = guests[i];

                if (predicate(currentGuest))
                {
                    guests.Insert(i + 1, currentGuest);
                    i++;
                }
            }
        }

        public static Predicate<string> GetPredicate(string differentCriteria, string @string)
        {
            Predicate<string> predicate = null;

            if (differentCriteria == "StartsWith")
            {
                predicate = new Predicate<string>((name) =>
                {
                    return name.StartsWith(@string);
                });
            }
            else if (differentCriteria == "EndsWith")
            {
                predicate = new Predicate<string>((name) =>
                {
                    return name.EndsWith(@string);
                });
            }
            else if (differentCriteria == "Length")
            {
                predicate = new Predicate<string>((name) =>
                {
                    return name.Length == int.Parse(@string);
                });
            }

            return predicate;
        }
    }
}
