using System;
using System.Linq;

namespace PizzaCalories
{
    public class StartUp
    {
        public static void Main()
        {
            try
            {
                string[] inputPizza = Console.ReadLine()
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                //if (string.IsNullOrEmpty(inputPizza[1]))
                //{
                //    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                //}

                string pizzaName = inputPizza[1];

                string[] doughInput = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string flourType = doughInput[1];
                string bakingTechnique = doughInput[2];
                decimal flourWeight = decimal.Parse(doughInput[3]);

                Pizza pizza = new Pizza(pizzaName);
                pizza.Dough = new Dough(flourType, bakingTechnique, flourWeight);

                string command = Console.ReadLine();

                while (command != "END")
                {
                    string[] toppingInput = command
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();
                    string toppingType = toppingInput[1];
                    decimal toppingWeight = decimal.Parse(toppingInput[2]);

                    Topping topping = new Topping(toppingType, toppingWeight);
                    pizza.AddTopping(topping);

                    command = Console.ReadLine();
                }

                Console.WriteLine(pizza);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return; // CARE
            }
        }
    }
}
