using System;
using System.Linq;
using System.Collections.Generic;

namespace ParkingLot
{
    class StartUp
    {
        static void Main()
        {
            HashSet<string> carNumbers = new HashSet<string>();

            string command = Console.ReadLine();

            while (command != "END")
            {
                string[] commandArguments = command
                    .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string direction = commandArguments[0];
                string carNumber = commandArguments[1];

                if (direction == "IN")
                {
                    carNumbers.Add(carNumber);
                }
                else if (direction == "OUT")
                {
                    carNumbers.Remove(carNumber);
                }

                command = Console.ReadLine();
            }

            if (carNumbers.Any())
            {
                foreach (var number in carNumbers)
                {
                    Console.WriteLine(number);
                }
            }
            else
            {
                Console.WriteLine("Parking Lot is Empty");
            }
        }
    }
}
