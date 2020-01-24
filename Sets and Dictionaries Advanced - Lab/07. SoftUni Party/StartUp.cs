using System;
using System.Linq;
using System.Collections.Generic;

namespace SoftUniParty
{
    class StartUp
    {
        static void Main()
        {
            HashSet<string> vip = new HashSet<string>();
            HashSet<string> regular = new HashSet<string>();

            bool isPartyStarted = false;

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                if (command == "PARTY")
                {
                    isPartyStarted = true;
                }

                if (!isPartyStarted && command.Length == 8)
                {
                    if (char.IsDigit(command[0]))
                    {
                        vip.Add(command);
                    }
                    else
                    {
                        regular.Add(command);
                    }
                }
                else
                {
                    vip.Remove(command);
                    regular.Remove(command);
                }
            }

            Console.WriteLine(vip.Count + regular.Count);

            if (vip.Any())
            {
                Console.WriteLine(string.Join(Environment.NewLine, vip));
            }

            if (regular.Any())
            {
                Console.WriteLine(string.Join(Environment.NewLine, regular));
            }
        }
    }
}
