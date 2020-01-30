using System;
using System.Linq;

namespace SumNumbers
{
    class StartUp
    {
        static void Main()
        {
            int[] inputNumber = Console.ReadLine()
                 .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            Console.WriteLine(inputNumber.Length);
            Console.WriteLine(inputNumber.Sum());
        }
    }
}
