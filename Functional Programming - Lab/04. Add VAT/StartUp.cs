using System;
using System.Linq;

namespace AddVAT
{
    class StartUp
    {
        static void Main()
        {
            double[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .Select(x=>x * 1.2)
                .ToArray();

            foreach (var price in inputNumbers)
            {
                Console.WriteLine($"{price:F2}");
            }
        }
    }
}
