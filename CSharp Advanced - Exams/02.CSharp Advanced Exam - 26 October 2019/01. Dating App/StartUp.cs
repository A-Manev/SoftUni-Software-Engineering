using System;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp
{
    class StartUp
    {
        static void Main()
        {
            int[] inputMales = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] inputFemales = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Stack<int> males = new Stack<int>(inputMales);
            Queue<int> females = new Queue<int>(inputFemales);

            int matchesCount = 0;

            while (males.Any() && females.Any())
            {
                int male = males.Peek();
                int female = females.Peek();

                if (male % 25 == 0 && male != 0 || female % 25 == 0 && female != 0)
                {
                    if (male % 25 == 0)
                    {
                        males.Pop();

                        if (males.Any())
                        {
                            males.Pop();
                        }
                    }
                    else
                    {
                        females.Dequeue();
                        if (females.Any())
                        {
                            females.Dequeue();
                        }
                    }
                    continue;
                }

                if (male <= 0)
                {
                    males.Pop();
                    continue;

                }

                if (female <= 0)
                {
                    females.Dequeue();
                    continue;
                }

                if (male == female)
                {
                    males.Pop();
                    females.Dequeue();
                    matchesCount++;
                }
                else
                {
                    males.Push(males.Pop() - 2);
                    females.Dequeue();
                }
            }

            Console.WriteLine($"Matches: {matchesCount}");

            if (!males.Any())
            {
                Console.WriteLine("Males left: none");
            }
            else
            {
                Console.WriteLine($"Males left: {string.Join(", ", males)}");
            }

            if (!females.Any())
            {
                Console.WriteLine("Females left: none");
            }
            else
            {
                Console.WriteLine($"Females left: {string.Join(", ", females)}");
            }
        }
    }
}
