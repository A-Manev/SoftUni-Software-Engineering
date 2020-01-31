using System;
using System.Linq;

namespace KnightsOfHonor
{
    class StartUp
    {
        static void Main()
        {
            string[] inputNames = Console.ReadLine()
                  .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                  .ToArray();

            /// First variations
            Action<string> appendSir = name => Console.WriteLine($"Sir {name}");

            /// First variations
            foreach (var name in inputNames)
            {
                appendSir(name);
            }

            /// Second variations
            //foreach (var name in inputNames)
            //{
            //    Action<string> print = Print(name);
            //    print(name);
            //}
        }

        /// Second variations
        //static Action<string> Print(string name)
        //{
        //    return name => Console.WriteLine("Sir " + name);
        //}
    }
}
