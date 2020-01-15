using System;
using System.Collections.Generic;

namespace HotPotato
{
    class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split();
            int toss = int.Parse(Console.ReadLine());

            Queue<string> childrens = new Queue<string>(input);

            int count = 1;

            while (childrens.Count != 1)
            {
                string child = childrens.Dequeue();

                if (count == toss)
                {
                    Console.WriteLine($"Removed {child}");
                    count = 0;
                }
                else
                {
                    childrens.Enqueue(child);
                }

                count++;
            }

            Console.WriteLine($"Last is {childrens.Dequeue()}");
        }
    }
}
