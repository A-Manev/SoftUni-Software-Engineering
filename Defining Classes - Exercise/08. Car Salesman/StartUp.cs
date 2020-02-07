using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesman
{
    public class StartUp
    {
        public static void Main()
        {
            HashSet<Engine> engines = new HashSet<Engine>();

            List<Car> cars = new List<Car>();

            int numberOfEngines = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfEngines; i++)
            {
                string[] inputEngine = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                Engine engine = null;

                string model = inputEngine[0];
                int power = int.Parse(inputEngine[1]);

                if (inputEngine.Length == 4)
                {
                    int displacement = int.Parse(inputEngine[2]);
                    string efficiency = inputEngine[3];

                    engine = new Engine(model, power, displacement, efficiency);
                }
                else if (inputEngine.Length == 3)
                {
                    int displacement;

                    bool isDisplacement = int.TryParse(inputEngine[2], out displacement);

                    if (isDisplacement)
                    {
                        engine = new Engine(model, power, displacement);
                    }
                    else
                    {
                        engine = new Engine(model, power, inputEngine[2]);
                    }
                }
                else if (inputEngine.Length == 2)
                {
                    engine = new Engine(model, power);
                }

                if (engine != null)
                {
                    engines.Add(engine);
                }
            }

            int numberOfCars = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCars; i++)
            {
                string[] inputCar = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                Car car = null;

                string model = inputCar[0];
                Engine engine = engines.First(x => x.Model == inputCar[1]);

                if (inputCar.Length == 2)
                {
                    car = new Car(model, engine);
                }
                else if (inputCar.Length == 3)
                {
                    double weight;

                    bool isWeight = double.TryParse(inputCar[2], out weight);

                    if (isWeight)
                    {
                        car = new Car(model, engine, weight);
                    }
                    else
                    {
                        car = new Car(model, engine, inputCar[2]);
                    }
                }
                else if (inputCar.Length == 4)
                {
                    double weight = double.Parse(inputCar[2]);
                    string color = inputCar[3];

                    car = new Car(model, engine, weight, color);
                }

                if (car != null)
                {
                    cars.Add(car);
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
