using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckTour
{
    class StartUp
    {
        static void Main()
        {
            int pumpsCount = int.Parse(Console.ReadLine());

            Queue<int[]> pumps = new Queue<int[]>();

            FillQueue(pumpsCount, pumps);

            int startIndex = 0;

            while (true)
            {
                int fuelAmount = 0;
                bool startPoint = true;

                foreach (var pump in pumps)
                {
                    fuelAmount += pump[0]; 

                    if (fuelAmount < pump[1])
                    {
                        startPoint = false;
                        break;
                    }

                    fuelAmount -= pump[1];
                }

                if (startPoint)
                {
                    break;
                }

                startIndex++;

                pumps.Enqueue(pumps.Dequeue());
            }

            Console.WriteLine(startIndex);
        }

        private static void FillQueue(int pumpsCount, Queue<int[]> pumps)
        {
            for (int i = 0; i < pumpsCount; i++)
            {
                int[] currentPump = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                pumps.Enqueue(currentPump);
            }
        }
    }
}
