using System;
using System.Linq;
using System.Collections.Generic;

namespace CitiesByContinentAndCountry
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, List<string>>> records = new Dictionary<string, Dictionary<string, List<string>>>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string continentName = command[0];
                string countryName = command[1];
                string cityName = command[2];

                if (!records.ContainsKey(continentName))
                {
                    records.Add(continentName, new Dictionary<string, List<string>>());
                }

                if (!records[continentName].ContainsKey(countryName))
                {
                    records[continentName].Add(countryName, new List<string>());
                }

                records[continentName][countryName].Add(cityName);
            }

            foreach (var continent in records)
            {
                Console.WriteLine($"{continent.Key}:");

                foreach (var (country, city) in continent.Value)
                {
                    Console.WriteLine($"  {country} -> {string.Join(", ", city)}");
                }
            }
        }
    }
}