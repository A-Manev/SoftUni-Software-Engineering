using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOperations
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int[] numberOfIntegers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            Stack<int> numbers = new Stack<int>();

            int n = input[0]; // N representing the number of elements to push into the stack
            int s = input[1]; // S representing the number of elements to pop from the stack
            int x = input[2]; // X representing element that you should look for in the stack.

            for (int i = 0; i < n; i++)
            {
                numbers.Push(numberOfIntegers[i]);
            }

            for (int i = 0; i < s; i++)
            {
                numbers.Pop();
            }

            if (numbers.Contains(x))
            {
                Console.WriteLine("true");
            }
            else if (numbers.Any())
            {
                Console.WriteLine(numbers.Min());
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}
