using System;
using System.Linq;
using System.Collections.Generic;

using WildFarm.Core.Contracts;
using WildFarm.Models.Animals;
using WildFarm.Models.Animals.Birds;
using WildFarm.Models.Animals.Mammals.Felines;
using WildFarm.Models.Animals.Mammals;
using WildFarm.Models.Animals.Contracts;
using WildFarm.Factories;
using WildFarm.Models.Foods.Contracts;
using WildFarm.Exceptions;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private ICollection<IAnimal> animals;
        private FoodFactory foodFactory; 

        public Engine()
        {
            this.animals = new List<IAnimal>();
            this.foodFactory = new FoodFactory();
        }

        public void Run()
        {
            string command = Console.ReadLine();

            while (command != "End")
            {
                string[] animalInformation = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] foodInformation = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                IAnimal animal = ProduceAnimal(animalInformation);

                string foodType = foodInformation[0];
                int foodQuantity = int.Parse(foodInformation[1]);

                IFood food = this.foodFactory.ProduceFood(foodType, foodQuantity);

                Console.WriteLine(animal.ProduceSound());

                this.animals.Add(animal);

                try
                {
                    animal.Feed(food);
                }
                catch (UneateableFoodException ufe)
                {
                    Console.WriteLine(ufe.Message);
                }

                command = Console.ReadLine();
            }

            foreach (var animal in this.animals)
            {
                Console.WriteLine(animal);
            }
        }

        private static IAnimal ProduceAnimal(string[] animalInformation)
        {
            IAnimal animal = null;

            string animalType = animalInformation[0];
            string animalName = animalInformation[1];
            double animalWeight = double.Parse(animalInformation[2]);

            if (animalType == "Hen")
            {
                double wingSize = double.Parse(animalInformation[3]);

                animal = new Hen(animalName, animalWeight, wingSize);
            }
            else if (animalType == "Owl")
            {
                double wingSize = double.Parse(animalInformation[3]);

                animal = new Owl(animalName, animalWeight, wingSize);
            }
            else
            {
                string livingRegion = animalInformation[3];

                if (animalType == "Dog")
                {
                    animal = new Dog(animalName, animalWeight, livingRegion);
                }
                else if (animalType == "Mouse")
                {
                    animal = new Mouse(animalName, animalWeight, livingRegion);
                }
                else
                {
                    string breed = animalInformation[4];

                    if (animalType == "Cat")
                    {
                        animal = new Cat(animalName, animalWeight, livingRegion, breed);
                    }
                    else if (animalType == "Tiger")
                    {
                        animal = new Tiger(animalName, animalWeight, livingRegion, breed);
                    }
                }
            }

            return animal;
        }
    }
}
