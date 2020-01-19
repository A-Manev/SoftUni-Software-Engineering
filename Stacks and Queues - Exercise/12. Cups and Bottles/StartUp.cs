using System;
using System.Linq;
using System.Collections.Generic;

namespace CupsAndBottles
{
    class StartUp
    {
        static void Main()
        {
            int[] cupsCapacity = Console.ReadLine()
                  .Split()
                  .Select(int.Parse)
                  .ToArray();
            int[] waterBottles = Console.ReadLine()
                 .Split()
                 .Select(int.Parse)
                 .ToArray();

            Stack<int> bottles = new Stack<int>(waterBottles);
            Stack<int> cups = new Stack<int>(cupsCapacity.Reverse());

            int wastedWater = 0;

            while (bottles.Any() && cups.Any())
            {
                int bottle = bottles.Pop();
                int cup = cups.Peek();

                if (cup <= bottle)
                {
                    wastedWater += bottle - cup;
                    cups.Pop();
                }
                else
                {
                    cups.Pop();
                    cups.Push(cup - bottle);
                }
            }

            if (bottles.Any())
            {
                Console.WriteLine($"Bottles: {string.Join(" ", bottles)}");
            }
            else
            {
                Console.WriteLine($"Cups: {string.Join(" ", cups)}");
            }

            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}
