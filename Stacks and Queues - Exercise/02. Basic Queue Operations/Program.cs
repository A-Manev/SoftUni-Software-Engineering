using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicQueueOperations
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

            Queue<int> numbers = new Queue<int>();

            int n = input[0]; // N representing the number of elements to enqueue (add) from the queue
            int s = input[1]; // S representing the number of elements to dequeue (remove) from the queue 
            int x = input[2]; // X representing element that you should look for in the queue

            for (int i = 0; i < n; i++)
            {
                numbers.Enqueue(numberOfIntegers[i]);
            }

            for (int i = 0; i < s && numbers.Any(); i++)
            {
                numbers.Dequeue();
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
