using System;
using System.Linq;

namespace Vehicles
{
    public class Program
    {
        public static void Main()
        {
            string[] inputCar = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            double carFuelQuantity = double.Parse(inputCar[1]);
            double carFuelConsumption = double.Parse(inputCar[2]);

            Vehicle car = new Car(carFuelQuantity, carFuelConsumption);

            string[] inputTruck = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            double truckFuelQuantity = double.Parse(inputTruck[1]);
            double truckFuelConsumption = double.Parse(inputTruck[2]);

            Vehicle truck = new Truck(truckFuelQuantity, truckFuelConsumption);

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] inputCommand = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (inputCommand[0] == "Drive")
                {
                    double distance = double.Parse(inputCommand[2]);

                    if (inputCommand[1] == "Car")
                    {
                        Console.WriteLine(car.Drive(distance));
                    }
                    else if (inputCommand[1] == "Truck")
                    {
                        Console.WriteLine(truck.Drive(distance));
                    }
                }
                else if (inputCommand[0] == "Refuel")
                {
                    double fuel = double.Parse(inputCommand[2]);

                    if (inputCommand[1] == "Car")
                    {
                        car.Refuel(fuel);
                    }
                    else if (inputCommand[1] == "Truck")
                    {
                        truck.Refuel(fuel);
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
