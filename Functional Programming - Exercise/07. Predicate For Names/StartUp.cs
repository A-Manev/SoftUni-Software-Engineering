using System;
using System.Linq;
using System.Collections.Generic;

namespace PredicateForNames
{
    class StartUp
    {
        static void Main()
        {
            int length = int.Parse(Console.ReadLine());
            List<string> names = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .ToList();

            Predicate<string> filterByLength  = name => name.Length > length;
            Action<List<string>> print = x => Console.WriteLine(string.Join(Environment.NewLine, x));

            names.RemoveAll(filterByLength);
            print(names);
        }
    }
}
