using System;

namespace GenericCountMethodDouble
{
    public class StartUp
    {
        public static void Main()
        {
            Box<double> box = new Box<double>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                double input = double.Parse(Console.ReadLine());

                box.Values.Add(input);
            }

            double elementToCompare = double.Parse(Console.ReadLine());

            int result = box.CountGreaterElements(elementToCompare);

            Console.WriteLine(result);
        }
    }
}
