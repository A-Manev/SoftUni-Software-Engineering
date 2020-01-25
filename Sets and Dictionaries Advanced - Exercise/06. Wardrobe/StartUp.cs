using System;
using System.Linq;
using System.Collections.Generic;

namespace Wardrobe
{
    class StartUp
    {
        static void Main()
        {

            Dictionary<string, List<Clothes>> records = new Dictionary<string, List<Clothes>>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine()
                    .Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string color = command[0];
                string[] items = command[1].Split(",");

                if (!records.ContainsKey(color))
                {
                    records.Add(color, new List<Clothes>());
                }

                for (int j = 0; j < items.Length; j++)
                {
                    string itemName = items[j];

                    if (!records[color].Any(x => x.Name == itemName))
                    {
                        Clothes clothes = new Clothes(itemName, 1);
                        records[color].Add(clothes);
                    }
                    else
                    {
                        Clothes currentClothing = records[color].FirstOrDefault(x => x.Name == itemName);
                        currentClothing.Quantity++;
                    }
                }
            }

            string[] wantedItem = Console.ReadLine().Split().ToArray();

            FoundClothing(records, wantedItem);

            PrintWardrobe(records);
        }

        private static void PrintWardrobe(Dictionary<string, List<Clothes>> records)
        {
            foreach (var color in records)
            {
                Console.WriteLine($"{color.Key} clothes:");

                foreach (var clothing in color.Value)
                {
                    Console.WriteLine($"* {clothing.Name} - {clothing.Quantity} {clothing.Found}");
                }
            }
        }

        private static void FoundClothing(Dictionary<string, List<Clothes>> records, string[] wantedItem)
        {
            string color = wantedItem[0];
            string clothingName = wantedItem[1];

            if (records.ContainsKey(color) && records[color].Any(x => x.Name == clothingName))
            {
                Clothes wantedClothing = records[color].FirstOrDefault(x => x.Name == clothingName);
                wantedClothing.Found = "(found!)";
            }
        }

        public class Clothes
        {
            public Clothes(string name, int quantity, string found = "")
            {
                Name = name;
                Quantity = quantity;
                Found = found;
            }

            public string Name { get; set; }

            public int Quantity { get; set; }

            public string Found { get; set; }
        }
    }
}
