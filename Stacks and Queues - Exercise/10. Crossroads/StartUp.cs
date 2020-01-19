using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossroads
{
    class StartUp
    {
        static void Main()
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int freeWindowDuration = int.Parse(Console.ReadLine());

            Queue<string> cars = new Queue<string>();

            string command = Console.ReadLine();

            int totalCarsPassed = 0;

            while (command != "END")
            {
                if (command == "green")
                {
                    int currentGreenLight = greenLightDuration;

                    while (currentGreenLight > 0 && cars.Any())
                    {
                        string currentCar = cars.Peek();

                        if (currentGreenLight - currentCar.Length > 0)
                        {
                            string car = cars.Dequeue();
                            totalCarsPassed++;
                            currentGreenLight -= car.Length;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (currentGreenLight > 0 && cars.Any())
                    {
                        currentGreenLight += freeWindowDuration;

                        string currentCar = cars.Peek();

                        if (currentGreenLight - currentCar.Length >= 0)
                        {
                            totalCarsPassed++;
                            cars.Dequeue();
                        }
                        else
                        {
                            Console.WriteLine("A crash happened!");
                            Console.WriteLine($"{currentCar} was hit at {currentCar[currentGreenLight]}.");
                            return;
                        }
                    }
                }
                else
                {
                    cars.Enqueue(command);
                }

                command = Console.ReadLine();
            }

            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{totalCarsPassed} total cars passed the crossroads.");
        }
    }
}
