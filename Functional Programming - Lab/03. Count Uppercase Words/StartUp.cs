using System;
using System.Linq;

namespace CountUppercaseWords
{
    class StartUp
    {
        static void Main()
        {
            string[] input = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .ToArray();

            Func<string, bool> isUpper = isStringUpperCase;

            foreach (var word in input)
            {
                if (isUpper(word))
                {
                    Console.WriteLine(word);
                }
            }
        }

        static bool isStringUpperCase(string inputString)
        {
            return char.IsUpper(inputString[0]) ? true : false;
        }
    }
}
