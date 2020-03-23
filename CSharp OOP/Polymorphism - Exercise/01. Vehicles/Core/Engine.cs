using System;
using System.Linq;

using Vehicles.Models;
using Vehicles.Factories;
using Vehicles.Core.Contracts;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private VehicleFactory vehicleFactory;

        public Engine()
        {
            this.vehicleFactory = new VehicleFactory();
        }

        public void Run()
        {
            Vehicle car = ProduceVehicle();
            Vehicle truck = ProduceVehicle();

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] inputCommand = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string commandType = inputCommand[0];
                string vehicleType = inputCommand[1];

                if (commandType == "Drive")
                {
                    double distance = double.Parse(inputCommand[2]);

                    if (vehicleType == "Car")
                    {
                        Console.WriteLine(car.Drive(distance));
                    }
                    else if (vehicleType == "Truck")
                    {
                        Console.WriteLine(truck.Drive(distance));
                    }
                }
                else if (commandType == "Refuel")
                {
                    double fuel = double.Parse(inputCommand[2]);

                    if (vehicleType == "Car")
                    {
                        car.Refuel(fuel);
                    }
                    else if (vehicleType == "Truck")
                    {
                        truck.Refuel(fuel);
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }

        private Vehicle ProduceVehicle()
        {
            string[] inputVehicle = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

            string vehicleType = inputVehicle[0];
            double fuelQuantity = double.Parse(inputVehicle[1]);
            double fuelConsumption = double.Parse(inputVehicle[2]);

            Vehicle vehicle = this.vehicleFactory.ProduceVehicle(vehicleType, fuelQuantity, fuelConsumption);

            return vehicle;
        }
    }
}
