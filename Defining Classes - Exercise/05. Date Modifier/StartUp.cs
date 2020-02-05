using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main()
        {
            List<DateTime> datas = new List<DateTime>();

            for (int i = 0; i < 2; i++)
            {
                string[] dataInput = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                int year = int.Parse(dataInput[0]);
                int month = int.Parse(dataInput[1]);
                int day = int.Parse(dataInput[2]);

                DateTime data = new DateTime(year, month, day);
                datas.Add(data);
            }

            DateModifier dateModifier = new DateModifier();

            long result = dateModifier.CalculateDifferenceBetweenTwoDatas(datas);

            Console.WriteLine(result);
        }
    }
}
