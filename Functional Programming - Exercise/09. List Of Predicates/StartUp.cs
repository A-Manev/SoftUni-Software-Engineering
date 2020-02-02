using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfPredicates
{
    class StartUp
    {
        static void Main()
        {
            int endofTheRange = int.Parse(Console.ReadLine());

            List<int> dividers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Distinct()
                .ToList();

            List<int> numbers = new List<int>();

            for (int i = 1; i <= endofTheRange; i++)
            {
                numbers.Add(i);
            }

            Action<List<int>> print = x => Console.WriteLine(string.Join(" ", x));

            for (int i = 0; i < dividers.Count; i++)
            {
                Predicate<int> predicate = x => x % dividers[i] != 0;
                numbers.RemoveAll(predicate);
            }

            print(numbers);
        }
    }
}
