using System;
using System.Linq;
using System.Collections.Generic;

namespace CountSymbols
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<char, int> symbols = new Dictionary<char, int>();

            string input = Console.ReadLine();

            for (int i = 0; i < input.Length; i++)
            {
                char symbol = input[i];

                if (!symbols.ContainsKey(symbol))
                {
                    symbols.Add(symbol, 0);
                }

                symbols[symbol]++;
            }

            foreach (var (symbol, times) in symbols.OrderBy(x=>x.Key))
            {
                Console.WriteLine($"{symbol}: {times} time/s");
            }
        }
    }
}
