using System;
using System.Collections.Generic;
using System.Linq;

namespace StackSum
{
    class Program
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            Stack<int> elements = new Stack<int>(numbers);

            while (true)
            {
                string[] command = Console.ReadLine().ToLower().Split();
                string commandArguments = command[0];

                if (commandArguments == "add")
                {
                    elements.Push(int.Parse(command[1]));
                    elements.Push(int.Parse(command[2]));
                }
                else if (commandArguments == "remove")
                {
                    int numbersForRemove = int.Parse(command[1]);
                    if (elements.Count >= numbersForRemove)
                    {
                        for (int i = 0; i < numbersForRemove; i++)
                        {
                            elements.Pop();
                        }
                    }
                }
                else if (commandArguments == "end")
                {
                    break;
                }
            }

            Console.WriteLine($"Sum: {elements.Sum()}");
        }
    }
}
