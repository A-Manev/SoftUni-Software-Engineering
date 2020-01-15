using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCalculator
{
    class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split();

            Stack<string> expression = new Stack<string>(input.Reverse());

            while (expression.Count > 1)
            {
                int firstNumber = int.Parse(expression.Pop());
                string operation = expression.Pop();
                int secondNumber = int.Parse(expression.Pop());

                int result = operation switch
                {
                    "+" => (firstNumber + secondNumber),
                    "-" => (firstNumber - secondNumber),
                };

                expression.Push(result.ToString());
            }

            Console.WriteLine(expression.Pop());
        }
    }
}
