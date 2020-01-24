using System;
using System.Linq;
using System.Collections.Generic;

namespace ProductShop
{
    class StartUp
    {
        static void Main()
        {
            string commandInput = Console.ReadLine();

            var shops = new Dictionary<string, Dictionary<string, double>>();

            while (commandInput != "Revision")
            {
                string[] commandArguments = commandInput
                    .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string shop = commandArguments[0];
                string product = commandArguments[1];
                double price = double.Parse(commandArguments[2]);

                if (!shops.ContainsKey(shop))
                {
                    shops.Add(shop, new Dictionary<string, double>());
                }

                if (!shops[shop].ContainsKey(product))
                {
                    shops[shop].Add(product, price);
                }

                commandInput = Console.ReadLine();
            }

            foreach (var shop in shops.OrderBy(x=>x.Key))
            {
                Console.WriteLine($"{shop.Key}->");

                foreach (var product in shop.Value)
                {
                    Console.WriteLine($"Product: {product.Key}, Price: {product.Value}");
                }
            }
        }
    }
}
