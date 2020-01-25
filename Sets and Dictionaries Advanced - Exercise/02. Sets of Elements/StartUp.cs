using System;
using System.Linq;
using System.Collections.Generic;

namespace SetsOfElements
{
    class StartUp
    {
        static void Main()
        {
            HashSet<int> firstSet = new HashSet<int>();
            HashSet<int> secondSet = new HashSet<int>();

            int[] input = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            int n = input[0];
            int m = input[1];

            for (int i = 0; i < n + m; i++)
            {
                int inputNumber = int.Parse(Console.ReadLine());

                if (i < n)
                {
                    firstSet.Add(inputNumber);
                }
                else
                {
                    secondSet.Add(inputNumber);
                }
            }

            foreach (var numOne in firstSet)
            {
                foreach (var numTwo in secondSet)
                {
                    if (numOne == numTwo)
                    {
                        Console.Write(numOne + " ");
                    }
                }
            }
        }
    }
}
