using System;

namespace GenericCountMethodString
{
    public class StartUp
    {
        public static void Main()
        {
            Box<string> box = new Box<string>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();

                box.Values.Add(input);
            }

            string elementToCompare = Console.ReadLine();

            int result = box.CountGreaterElements(elementToCompare);

            Console.WriteLine(result);
        }
    }
}
