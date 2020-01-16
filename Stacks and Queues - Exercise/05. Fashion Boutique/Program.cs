using System;
using System.Collections.Generic;
using System.Linq;

namespace FashionBoutique
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int rackCapacity = int.Parse(Console.ReadLine());

            Stack<int> clothes = new Stack<int>(input);

            int racksCount = 1;
            int capacity = rackCapacity;

            while (clothes.Any())
            {
                int item = clothes.Peek();

                if (capacity - item >= 0)
                {
                    capacity -= item;
                    clothes.Pop();
                }
                else
                {
                    racksCount++;
                    capacity = rackCapacity;
                }
            }

            Console.WriteLine(racksCount);
        }
    }
}
