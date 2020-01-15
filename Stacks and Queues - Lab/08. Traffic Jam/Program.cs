using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficJam
{
    class Program
    {
        static void Main()
        {
            Queue<string> cars = new Queue<string>();
            int totalCars = 0;

            int n = int.Parse(Console.ReadLine());

            string command = Console.ReadLine();

            while (command != "end")
            {
                if (command == "green")
                {
                    for (int i = 0; i < n && cars.Any(); i++)
                    {
                        Console.WriteLine($"{cars.Dequeue()} passed!");
                        totalCars++;
                    }
                }
                else
                {
                    cars.Enqueue(command);
                }

                command = Console.ReadLine();
            }

            Console.WriteLine($"{totalCars} cars passed the crossroads.");
        }
    }
}
