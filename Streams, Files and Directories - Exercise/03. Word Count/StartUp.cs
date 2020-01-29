using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace WordCount
{
    class StartUp
    {
        static void Main()
        {
            string[] inputWords = File
                .ReadAllLines("./words.txt");

            string[] inputText = File
                .ReadAllText("./text.txt")
                .ToLower()
                .Split(new char[] { ' ', '.', ',', '!', '?', '-' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> words = new Dictionary<string, int>();

            int counter = 0;

            foreach (var currentWord in inputWords)
            {
                foreach (var word in inputText)
                {
                    if (currentWord == word)
                    {
                        counter++;
                    }
                }

                words[currentWord] = counter;
                counter = 0;
            }

            words = words.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var (word, count) in words)
            {
                File.AppendAllText("../../../actualResult.txt", $"{word} - {count}{Environment.NewLine}");
            }
        }
    }
}
