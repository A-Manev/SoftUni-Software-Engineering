using System;
using System.Linq;

namespace Threeuple
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

            string town = firstInputInfo.Length == 5 ? $"{firstInputInfo[3]} {firstInputInfo[4]}" : firstInputInfo[3];

            string[] secondInputInfo = Console.ReadLine()
                .Split()
                .ToArray();

            string name = secondInputInfo[0];
            int amountOfBeer = int.Parse(secondInputInfo[1]);
            bool isDrunk = secondInputInfo[2] == "drunk" ? true : false;

            string[] thirdInputInfo = Console.ReadLine()
               .Split()
               .ToArray();

            string inputName = thirdInputInfo[0];
            double doubleNumber = double.Parse(thirdInputInfo[1]);
            string bankName = thirdInputInfo[2];

            Threeuple<string, string, string> firstTuple = new Threeuple<string, string, string>(fullName, address, town);
            Threeuple<string, int, bool> secondTuple = new Threeuple<string, int, bool>(name, amountOfBeer, isDrunk);
            Threeuple<string, double, string> thirdTuple = new Threeuple<string, double, string>(inputName, doubleNumber, bankName);

            Console.WriteLine(firstTuple);
            Console.WriteLine(secondTuple);
            Console.WriteLine(thirdTuple);
        }
    }
}
