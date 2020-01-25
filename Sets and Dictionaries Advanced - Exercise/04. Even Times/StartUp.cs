using System;
using System.Collections.Generic;
using System.Linq;

namespace EvenTimes
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<int, int> numbers = new Dictionary<int, int>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                int inputNumber = int.Parse(Console.ReadLine());

                if (!numbers.ContainsKey(inputNumber))
                {
                    numbers.Add(inputNumber , 0);
                }

                numbers[inputNumber]++;
            }

            foreach (var num in numbers.Where(x=>x.Value % 2 == 0))
            {
                Console.WriteLine(num.Key);
            }
        }
    }
}
