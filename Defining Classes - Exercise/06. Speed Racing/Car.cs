using System;

namespace SpeedRacing
{
    public class Car
    {
        public Car(string model, decimal fuelAmount, decimal fuelConsumptionPerKilometer, decimal travelledDistance = 0)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumptionPerKilometer = fuelConsumptionPerKilometer;
            TravelledDistance = travelledDistance;
        }

        public string Model { get; set; }

        public decimal FuelAmount { get; set; }

        public decimal FuelConsumptionPerKilometer { get; set; }

        public decimal TravelledDistance { get; set; }

        public void Drive(decimal distance)
        {
           decimal neededFuel = distance * FuelConsumptionPerKilometer;

            if (neededFuel <= FuelAmount)
            {
                FuelAmount -= neededFuel;
                TravelledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }
    }
}
