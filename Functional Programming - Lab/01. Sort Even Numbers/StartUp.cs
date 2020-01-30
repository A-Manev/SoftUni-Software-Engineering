using System;
using System.Linq;

namespace SortEvenNumbers
{
    class StartUp
    {
        static void Main()
        {
            int[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(x => x % 2 == 0)
                .OrderBy(x => x)
                .ToArray();

            Console.WriteLine(string.Join(", ", inputNumbers));
        }
    }
}
