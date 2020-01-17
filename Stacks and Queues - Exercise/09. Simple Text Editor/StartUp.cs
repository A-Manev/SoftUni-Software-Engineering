using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTextEditor
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            string text = string.Empty;

            Stack<string> latestVersion = new Stack<string>();

            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine()
                    .Split()
                    .ToArray();

                string cmdArgs = command[0];

                if (cmdArgs == "1")
                {
                    latestVersion.Push(text);
                    string someString = command[1];
                    text = text + someString;
                }
                else if (cmdArgs == "2")
                {
                    latestVersion.Push(text);
                    int count = int.Parse(command[1]);
                    int startIndex = text.Length - count;
                    text = text.Remove(startIndex);
                }
                else if (cmdArgs == "3")
                {
                    int index = int.Parse(command[1]);

                    Console.WriteLine(text[index - 1]);
                }
                else if (cmdArgs == "4")
                {
                    string update = latestVersion.Pop();
                    text = update;
                }
            }
        }
    }
}
