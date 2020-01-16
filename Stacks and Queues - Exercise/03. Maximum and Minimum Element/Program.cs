using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumAndMinimumElement
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                int[] input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                int numberType = input[0];

                if (numberType == 1)
                {
                    int element = input[1];
                    stack.Push(element);
                }
                else if (numberType == 2 && stack.Any())
                {
                    stack.Pop();
                }
                else if (numberType == 3 && stack.Any())
                {
                    Console.WriteLine(stack.Max());
                }
                else if (numberType == 4 && stack.Any())
                {
                    Console.WriteLine(stack.Min());
                }
            }

            Console.WriteLine(string.Join(", ", stack));
        }
    }
}
