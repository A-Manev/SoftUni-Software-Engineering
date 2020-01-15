using System;
using System.Collections.Generic;

namespace MatchingBrackets
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char symbol = input[i];

                if (symbol == '(')
                {
                    stack.Push(i);
                }
                else if (symbol == ')')
                {
                    int startIndex = stack.Pop();
                    int lenght = i - startIndex + 1;
                    string expresion = input.Substring(startIndex, lenght);

                    Console.WriteLine(expresion);
                }
            }
        }
    }
}
