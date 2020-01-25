using System;
using System.Linq;
using System.Collections.Generic;

namespace PeriodicTable
{
    class StartUp
    {
        static void Main()
        {
            HashSet<string> chemicalElements = new HashSet<string>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] elements = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int j = 0; j < elements.Length; j++)
                {
                    chemicalElements.Add(elements[j]);
                }
            }

            Console.WriteLine(string.Join(" ", chemicalElements.OrderBy(x => x)));
        }
    }
}
