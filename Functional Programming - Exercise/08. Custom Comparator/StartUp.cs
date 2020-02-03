using System;
using System.Linq;

namespace CustomComparator
{
    class StartUp
    {
        static void Main()
        {
            Func<int, int, int> comparator = new Func<int, int, int>((a, b) =>
            {
                if (a % 2 == 0 && b % 2 != 0)
                {
                    return -1;
                }
                else if (a % 2 != 0 && b % 2 == 0)
                {
                    return 1;
                }
                else
                {
                    return a.CompareTo(b);
                }
            });

            Comparison<int> comparison = new Comparison<int>(comparator);

            int[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Array.Sort(inputNumbers, comparison);

            Console.WriteLine(string.Join(" ", inputNumbers));
        }
    }
}
