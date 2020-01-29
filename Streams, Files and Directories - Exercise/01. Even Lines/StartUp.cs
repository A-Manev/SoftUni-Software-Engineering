using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EvenLines
{
    class StartUp
    {
        static void Main()
        {
            char[] charactersToReplace = { '-', ',', '.', '!', '?' };

            using (StreamReader streamReader = new StreamReader("./text.txt"))
            {
                int counter = 0;

                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    if (counter % 2 == 0)
                    {
                        line = ReplaceAll(charactersToReplace, '@', line);
                        line = ReverseString(line);

                        Console.WriteLine(line);
                    }

                    counter++;
                }
            }
        }

        static string ReplaceAll(char[] replace, char replacement, string line)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char currentSymbol = line[i];

                if (replace.Contains(currentSymbol))
                {
                    stringBuilder.Append('@');
                }
                else
                {
                    stringBuilder.Append(currentSymbol);
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ReverseString(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] words = s.Split().ToArray();

            for (int i = 0; i < words.Length; i++)
            {
                stringBuilder.Append(words[words.Length - i - 1]);
                stringBuilder.Append(" ");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
