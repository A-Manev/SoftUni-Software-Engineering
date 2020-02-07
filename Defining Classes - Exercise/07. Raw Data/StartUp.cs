using System;
using System.Linq;
using System.Collections.Generic;

namespace RawData
{
    public class StartUp
    {
        public static void Main()
        {
            List<Car> cars = new List<Car>();

            int numberOfCars = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCars; i++)
            {
                string[] inputCar = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string model = inputCar[0];
                int engineSpeed = int.Parse(inputCar[1]);
                int enginePower = int.Parse(inputCar[2]);
                int cargoWeight = int.Parse(inputCar[3]);
                string cargoType = inputCar[4];

                double tire1Pressure = double.Parse(inputCar[5]);
                int tire1Age = int.Parse(inputCar[6]);
                double tire2Pressure = double.Parse(inputCar[7]);
                int tire2Age = int.Parse(inputCar[8]);
                double tire3Pressure = double.Parse(inputCar[9]);
                int tire3Age = int.Parse(inputCar[10]);
                double tire4Pressure = double.Parse(inputCar[11]);
                int tire4Age = int.Parse(inputCar[12]);

                Engine engine = new Engine(engineSpeed, enginePower);

                Cargo cargo = new Cargo(cargoWeight, cargoType);

                Tire[] tires = new Tire[4]
                {
                    new Tire(tire1Age, tire1Pressure),
                    new Tire(tire2Age, tire2Pressure),
                    new Tire(tire3Age, tire3Pressure),
                    new Tire(tire4Age, tire4Pressure),
                };

                Car car = new Car(model, engine, cargo, tires);

                cars.Add(car);
            }

            string command = Console.ReadLine();


            if (command == "fragile")
            {
                List<Car> myCars = new List<Car>();

                var targetCar = cars.FindAll(x => x.Cargo.Type == "fragile");

                foreach (var car in targetCar)
                {
                    foreach (var pressure in car.Tires)
                    {
                        if (pressure.Pressure < 1)
                        {
                            myCars.Add(car);
                            break;
                        }
                    }
                }

                PrintCarModel(myCars);
            }
            else if (command == "flamable")
            {
                List<Car> myCars = new List<Car>();

                var targetCar = cars.FindAll(x => x.Cargo.Type == "flamable");

                foreach (var car in targetCar)
                {
                    if (car.Engine.Power > 250)
                    {
                        myCars.Add(car);
                    }
                }

                PrintCarModel(myCars);
            }
        }

        private static void PrintCarModel(List<Car> myCars)
        {
            foreach (var car in myCars)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}
