using System;

using Vehicles.Models;

namespace Vehicles.Factories
{
    public class VehicleFactory
    {
        public Vehicle ProduceVehicle(string vehicleType, double fuelQuantity, double fuelConsumption)
        {
            string baseNamespace = "Vehicles.Models";

            Type type = Type.GetType($"{baseNamespace}.{vehicleType}");

            Vehicle vehicle = (Vehicle)Activator.CreateInstance(type, fuelQuantity, fuelConsumption);

            return vehicle;
        }
    }
}
