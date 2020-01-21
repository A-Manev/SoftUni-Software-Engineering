using System;
using System.Linq;

namespace MatrixShuffling
{
    class StartUp
    {
        static void Main()
        {
            int[] dimensions = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int rows = dimensions[0];
            int cols = dimensions[1];

            string[,] matrix = InitializeMatrix(rows, cols);

            string command = Console.ReadLine();

            while (command != "END")
            {
                string[] cmdArgs = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (cmdArgs.Length == 5)
                {
                    IsInside(matrix, cmdArgs);
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

                command = Console.ReadLine();
            }
        }

        private static string[,] InitializeMatrix(int rows, int cols)
        {
            string[,] matrix = new string[rows, cols];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            return matrix;
        }

        private static void IsInside(string[,] matrix, string[] cmdArgs)
        {
            int row1 = int.Parse(cmdArgs[1]);
            int col1 = int.Parse(cmdArgs[2]);
            int row2 = int.Parse(cmdArgs[3]);
            int col2 = int.Parse(cmdArgs[4]);

            if (row1 >= 0 && row1 < matrix.GetLength(0) &&
                col1 >= 0 && col1 < matrix.GetLength(1) &&
                row2 >= 0 && row2 < matrix.GetLength(0) &&
                col2 >= 0 && col2 < matrix.GetLength(1))
            {
                SlapCells(matrix, row1, col1, row2, col2);

                PrintMatrix(matrix);
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void SlapCells(string[,] matrix, int row1, int col1, int row2, int col2)
        {
            string swap = matrix[row1, col1];
            matrix[row1, col1] = matrix[row2, col2];
            matrix[row2, col2] = swap;
        }

        private static void PrintMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
