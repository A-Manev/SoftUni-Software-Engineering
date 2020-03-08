using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        public static void Main()
        {
			try
			{
                List<Person> people = new List<Person>();
                List<Product> products = new List<Product>();

                string[] inputPeople = Console.ReadLine()
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] inputProducts = Console.ReadLine()
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int i = 0; i < inputPeople.Length; i++)
                {
                    string[] peopleArguments = inputPeople[i]
                        .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    string name = peopleArguments[0];
                    decimal money = decimal.Parse(peopleArguments[1]);

                    Person person = new Person(name, money);

                    people.Add(person);
                }

                for (int i = 0; i < inputProducts.Length; i++)
                {
                    string[] productsArguments = inputProducts[i]
                        .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    string name = productsArguments[0];
                    decimal cost = decimal.Parse(productsArguments[1]);

                    Product product = new Product(name, cost);

                    products.Add(product);
                }

                string command = Console.ReadLine();

                while (command != "END")
                {
                    string[] commandArguments = command
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    string personName = commandArguments[0];
                    string productName = commandArguments[1];

                    Person currentPerson = people.FirstOrDefault(x => x.Name == personName);
                    Product currentProduct = products.FirstOrDefault(x => x.Name == productName);

                    currentPerson.BuyProduct(currentProduct);

                    command = Console.ReadLine();
                }

                foreach (var person in people)
                {
                    Console.WriteLine(person);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
