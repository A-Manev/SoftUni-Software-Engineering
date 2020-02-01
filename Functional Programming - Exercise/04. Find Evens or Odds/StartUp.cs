using System;
using System.Linq;
using System.Collections.Generic;

namespace FindEvensOrOdds
{
    class StartUp
    {
        static void Main()
        {
            Predicate<int> isEven = x => x % 2 == 0;
            Action<List<int>> print = x => Console.WriteLine(string.Join(" ", x));

            int[] inputRange = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            string condition = Console.ReadLine();

            int start = inputRange[0];
            int end = inputRange[1];

            List<int> numbers = new List<int>();

            for (int i = start; i <= end; i++)
            {
                numbers.Add(i);
            }

            if (condition == "odd")
            {
                numbers.RemoveAll(isEven);
            }
            else if (condition == "even")
            {
                numbers.RemoveAll(x => !isEven(x));
            }

            print(numbers);
        }
    }
}
