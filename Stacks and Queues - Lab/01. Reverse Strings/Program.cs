using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseStrings
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();

            Stack<char> text = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                text.Push(input[i]);
            }

            while (text.Any())
            {
                Console.Write(text.Pop());
            }
        }
    }
}
