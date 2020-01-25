using System;
using System.Collections.Generic;

namespace UniqueUsernames
{
    class StartUp
    {
        static void Main()
        {
            HashSet<string> usernames = new HashSet<string>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string inputUsername = Console.ReadLine();

                usernames.Add(inputUsername);
            }

            Console.WriteLine(string.Join(Environment.NewLine, usernames));
        }
    }
}
