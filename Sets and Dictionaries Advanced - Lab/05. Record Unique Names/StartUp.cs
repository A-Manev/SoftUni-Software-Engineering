using System;
using System.Collections.Generic;

namespace RecordUniqueNames
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            HashSet<string> names = new HashSet<string>();

            for (int i = 0; i < n; i++)
            {
                string command = Console.ReadLine();

                names.Add(command);
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
