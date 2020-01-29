using System;
using System.IO;

namespace LineNumbers
{
    class StartUp
    {
        static void Main()
        {
            string[] inputLines = File.ReadAllLines("./text.txt");
            string[] outputLines = new string[inputLines.Length];

            for (int i = 0; i < inputLines.Length; i++)
            {
                string currentLine = inputLines[i];

                int lettersCount = CountLetters(currentLine);
                int punctuationMarksCount = CountPunctuationMarks(currentLine);

                outputLines[i] = $"Line {i + 1}: {currentLine} ({lettersCount})({punctuationMarksCount})";
            }

            File.WriteAllLines("../../../output.txt", outputLines);
        }

        private static int CountPunctuationMarks(string currentLine)
        {
            int punctuationMarksCount = 0;
            for (int j = 0; j < currentLine.Length; j++)
            {
                char currentSymbol = currentLine[j];

                if (char.IsPunctuation(currentSymbol))
                {
                    punctuationMarksCount++;
                }
            }

            return punctuationMarksCount;
        }

        private static int CountLetters(string currentLine)
        {
            int lettersCount = 0;

            for (int j = 0; j < currentLine.Length; j++)
            {
                char currentSymbol = currentLine[j];

                if (char.IsLetter(currentSymbol))
                {
                    lettersCount++;
                }
            }

            return lettersCount;
        }
    }
}
