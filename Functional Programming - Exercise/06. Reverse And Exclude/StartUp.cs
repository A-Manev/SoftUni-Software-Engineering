using System;
using System.Linq;
using System.Collections.Generic;

namespace ReverseAndExclude
{
    class StartUp
    {
        static void Main()
        {
            List<int> elements = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToList();

            int divider = int.Parse(Console.ReadLine());

            Predicate<int> isDevisible = x => x % divider == 0;
            Action<List<int>> print = x => Console.WriteLine(string.Join(" ", x));

            elements.Reverse();
            elements.RemoveAll(isDevisible);
            print(elements);
        }
    }
}
