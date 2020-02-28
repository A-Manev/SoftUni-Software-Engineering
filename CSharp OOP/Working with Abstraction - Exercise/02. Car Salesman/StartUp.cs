using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesman
{
    public class StartUp
    {
        public static void Main()
        {
            List<Car> cars = new List<Car>();
            List<Engine> engines = new List<Engine>();

            int numberOfEngines = int.Parse(Console.ReadLine());
            AddEngine(engines, numberOfEngines);

            int numberOfCars = int.Parse(Console.ReadLine());

            AddCar(cars, engines, numberOfCars);

            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        private static void AddCar(List<Car> cars, List<Engine> engines, int numberOfCars)
        {
            for (int i = 0; i < numberOfCars; i++)
            {
                string[] inputCar = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string model = inputCar[0];
                string engineModel = inputCar[1];

                Car car = null;

                Engine engine = engines.First(x => x.Model == engineModel);

                double weight;

                if (inputCar.Length == 2)
                {
                    car = new Car(model, engine);
                }
                else if (inputCar.Length == 3)
                {
                    bool success = double.TryParse(inputCar[2], out weight);

                    car = success
                        ? new Car(model, engine, weight)
                        : new Car(model, engine, inputCar[2]);
                }
                else if (inputCar.Length == 4)
                {
                    weight = double.Parse(inputCar[2]);
                    string color = inputCar[3];

                    car = new Car(model, engine, weight, color);
                }

                cars.Add(car);
            }
        }

        private static void AddEngine(List<Engine> engines, int numberOfEngines)
        {
            for (int i = 0; i < numberOfEngines; i++)
            {
                string[] inputEngine = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string model = inputEngine[0];
                int power = int.Parse(inputEngine[1]);

                Engine engine = null;

                int displacement;

                if (inputEngine.Length == 2)
                {
                    engine = new Engine(model, power);
                }
                else if (inputEngine.Length == 3)
                {
                    bool success = int.TryParse(inputEngine[2], out displacement);

                    engine = success
                        ? new Engine(model, power, displacement)
                        : new Engine(model, power, inputEngine[2]);

                }
                else if (inputEngine.Length == 4)
                {
                    displacement = int.Parse(inputEngine[2]);
                    string efficiency = inputEngine[3];

                    engine = new Engine(model, power, displacement, efficiency);
                }

                engines.Add(engine);
            }
        }
    }
}
