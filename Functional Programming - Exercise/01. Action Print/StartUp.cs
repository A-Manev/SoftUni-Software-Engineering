using System;
using System.Linq;

namespace ActionPrint
{
    class StartUp
    {
        static void Main()
        {
            string[] inputNames = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            /// First variations
            Action<string[]> print = name => Console.WriteLine(string.Join(Environment.NewLine, name));
            print(inputNames);

            /// Second variations
            //Action<string[]> print = Print;
            //print(inputNames);

            /// Third variations
            //foreach (var name in inputNames)
            //{
            //    Action<string> writer = Writer(name);

            //    writer(name);
            //}
        }

        /// Second variations
        //static void Print(string[] names)
        //{
        //    Console.WriteLine(string.Join(Environment.NewLine, names));
        //}

        /// Third variations
        //static Action<string> Writer(string name)
        //{
        //    return x => Console.WriteLine(x);
        //}
    }
}
