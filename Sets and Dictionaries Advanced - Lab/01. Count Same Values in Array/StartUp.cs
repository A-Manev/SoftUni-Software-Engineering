using System;
using System.Linq;
using System.Collections.Generic;

namespace CountSameValueInArray
{
    class StartUp
    {
        static void Main()
        {
            double[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Dictionary<double, int> numbers = new Dictionary<double, int>();

            for (int i = 0; i < inputNumbers.Length; i++)
            {
                double currentNumber = inputNumbers[i];

                if (!numbers.ContainsKey(currentNumber))
                {
                    numbers.Add(currentNumber, 0);
                }

                numbers[currentNumber]++;
            }

            foreach (var (key,value) in numbers)
            {
                Console.WriteLine($"{key} - {value} times");
            }
        }
    }
}
