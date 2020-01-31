using System;
using System.Linq;

namespace CustomMinFunction
{
    class StartUp
    {
        static void Main()
        {
            int[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            /// First variations
            //Func<int[], int> minFunc = x => x.Min();
            //Console.WriteLine(minFunc(inputNumbers));

            /// Second variations
            Func<int[], int> minFunc = FindSmallestNumber;

            int minNumber = minFunc(inputNumbers);

            Console.WriteLine(minNumber);
        }

        /// Second variations
        private static int FindSmallestNumber(int[] numbers)
        {
            int minNumber = int.MaxValue;

            foreach (var number in numbers)
            {
                if (number < minNumber)
                {
                    minNumber = number;
                }
            }

            return minNumber;
        }
    }
}
