using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManufacturer
{
    public class StartUp
    {
        public static void Main()
        {
            List<Tire[]> tires = new List<Tire[]>();

            string inputTiresInfo = Console.ReadLine();

            while (inputTiresInfo != "No more tires")
            {
                double[] tiresInfo = inputTiresInfo
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToArray();

                var currentTires = new Tire[4]
                {
                    new Tire((int)tiresInfo[0], tiresInfo[1]),
                    new Tire((int)tiresInfo[2], tiresInfo[3]),
                    new Tire((int)tiresInfo[4], tiresInfo[5]),
                    new Tire((int)tiresInfo[6], tiresInfo[7]),
                };

                tires.Add(currentTires);

                inputTiresInfo = Console.ReadLine();
            }

            List<Engine> engines = new List<Engine>();

            string inputEngineInfo = Console.ReadLine();

            while (inputEngineInfo != "Engines done")
            {
                string[] engineInfo = inputEngineInfo
                   .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .ToArray();

                int horsePower = int.Parse(engineInfo[0]);
                double cubicCapacity = double.Parse(engineInfo[1]);

                Engine engine = new Engine(horsePower, cubicCapacity);

                engines.Add(engine);

                inputEngineInfo = Console.ReadLine();
            }

            string inputCarInfo = Console.ReadLine();

            List<Car> cars = new List<Car>();

            while (inputCarInfo != "Show special")
            {
                string[] carInfo = inputCarInfo
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string make = carInfo[0];
                string model = carInfo[1];
                int year = int.Parse(carInfo[2]);
                double fuelQuantity = double.Parse(carInfo[3]);
                double fuelConsumption = double.Parse(carInfo[4]);
                int engineIndex = int.Parse(carInfo[5]);
                int tiresIndex = int.Parse(carInfo[6]);

                Car car = new Car(make, model, year, fuelQuantity, fuelConsumption, engines[engineIndex], tires[tiresIndex]);

                cars.Add(car);

                inputCarInfo = Console.ReadLine();
            }

            foreach (var currentCar in cars)
            {
                bool checkYear = currentCar.Year >= 2017;
                bool checkHorsePower = currentCar.Engine.HorsePower > 330;
                double totalPressure = 0;

                foreach (var currentPressure in currentCar.Tires)
                {
                    totalPressure += currentPressure.Pressure;
                }
                bool checkPressure = totalPressure >= 9 && totalPressure <= 10;

                if (checkYear && checkHorsePower && checkPressure)
                {
                    currentCar.Drive(20);

                    Console.WriteLine(currentCar.CarInfo());
                }
            }
        }
    }
}
