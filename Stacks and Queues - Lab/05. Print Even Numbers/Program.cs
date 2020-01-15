using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintEvenNumbers
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            Queue<int> numbers = new Queue<int>();

            foreach (var number in input)
            {
                numbers.Enqueue(number);
            }

            Console.WriteLine(string.Join(", ", numbers.Where(x => x % 2 == 0)));
        }
    }
}
