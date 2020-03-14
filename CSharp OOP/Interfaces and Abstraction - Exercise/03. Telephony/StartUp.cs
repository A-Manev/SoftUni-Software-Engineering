using System;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        public static void Main()
        {
            IBrowseable smartphone = new Smartphone();
            ICallable stationaryPhone = new StationaryPhone();

            string[] inputNumbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string[] inputSites = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            for (int i = 0; i < inputNumbers.Length; i++)
            {
                string number = inputNumbers[i];

                if (number.Length == 10)
                {
                    Console.WriteLine(smartphone.Call(inputNumbers[i]));
                }
                else
                {
                    Console.WriteLine(stationaryPhone.Call(inputNumbers[i]));
                }
            }

            for (int i = 0; i < inputSites.Length; i++)
            {
                string site = inputSites[i];

                Console.WriteLine(smartphone.Browse(site));
            }
        }
    }
}
