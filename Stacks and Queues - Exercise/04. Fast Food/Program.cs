using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFood
{
    class Program
    {
        static void Main()
        {
            int foodQuantity = int.Parse(Console.ReadLine());

            int[] orders = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            Queue<int> clientsQueue = new Queue<int>(orders);

            Console.WriteLine(clientsQueue.Max());

            while (foodQuantity >= 0 && clientsQueue.Any())
            {
                int client = clientsQueue.Peek();

                if (foodQuantity - client >= 0)
                {
                    foodQuantity -= clientsQueue.Dequeue();
                }
                else
                {
                    break;
                }
            }

            if (clientsQueue.Count != 0 )
            {
                Console.WriteLine($"Orders left: {string.Join(" ", clientsQueue)}");
            }
            else
            {
                Console.WriteLine("Orders complete");
            }
        }
    }
}
