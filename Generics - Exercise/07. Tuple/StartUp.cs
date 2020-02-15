using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuple
{
    public class StartUp
    {
        public static void Main()
        {
            string[] firstInputInfo = Console.ReadLine()
                .Split()
                .ToArray();

            string fullName = $"{firstInputInfo[0]} {firstInputInfo[1]}";
            string address = firstInputInfo[2];

            string[] secondInputInfo = Console.ReadLine()
                .Split()
                .ToArray();

            string name = secondInputInfo[0];
            int amountOfBeer = int.Parse(secondInputInfo[1]);

            string[] thirdInputInfo = Console.ReadLine()
               .Split()
               .ToArray();

            int integerNumber = int.Parse(thirdInputInfo[0]);
            double doubleNumber = double.Parse(thirdInputInfo[1]);

            Tuple<string, string> firstTuple = new Tuple<string, string>(fullName, address);
            Tuple<string, int> secondTuple = new Tuple<string, int>(name, amountOfBeer);
            Tuple<int, double> thirdTuple = new Tuple<int, double>(integerNumber, doubleNumber);

            Console.WriteLine(firstTuple);
            Console.WriteLine(secondTuple);
            Console.WriteLine(thirdTuple);
        }
    }
}
