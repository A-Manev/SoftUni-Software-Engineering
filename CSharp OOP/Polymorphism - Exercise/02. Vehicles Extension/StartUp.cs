using System;
using System.Linq;

namespace Vehicles
{
    public class StartUp
    {
        public static void Main()
        {
            string[] inputCar = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            double carFuelQuantity = double.Parse(inputCar[1]);
            double carFuelConsumption = double.Parse(inputCar[2]);
            double carTankCapacity = double.Parse(inputCar[3]);

            Vehicle car = new Car(carFuelQuantity, carFuelConsumption, carTankCapacity);

            string[] inputTruck = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            double truckFuelQuantity = double.Parse(inputTruck[1]);
            double truckFuelConsumption = double.Parse(inputTruck[2]);
            double truckTankCapacity = double.Parse(inputTruck[3]);

            Vehicle truck = new Truck(truckFuelQuantity, truckFuelConsumption, truckTankCapacity);

            string[] inputBus = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            double busFuelQuantity = double.Parse(inputBus[1]);
            double busFuelConsumption = double.Parse(inputBus[2]);
            double busTankCapacity = double.Parse(inputBus[3]);

            Bus bus = new Bus(busFuelQuantity, busFuelConsumption, busTankCapacity);

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] inputCommand = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (inputCommand[0] == "DriveEmpty")
                {
                    double distance = double.Parse(inputCommand[2]);

                    if (inputCommand[1] == "Bus")
                    {
                        Console.WriteLine(bus.DriveEmpty(distance));
                    }
                }

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
                    else if(inputCommand[1] == "Bus")
                    {
                        Console.WriteLine(bus.Drive(distance));
                    }
                }
                else if (inputCommand[0] == "Refuel")
                {
                    try
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
                        else if(inputCommand[1] == "Bus")
                        {
                            bus.Refuel(fuel);
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
            Console.WriteLine(bus);
        }
    }
}
