using System;
using System.Linq;

namespace SquaresInMatrix
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

            char[,] matrix = InitializeMatrix(rows, cols);

            int equalSquaresCellsCount = FindSquaresWithEqualChars(matrix);

            Console.WriteLine(equalSquaresCellsCount);
        }

        private static int FindSquaresWithEqualChars(char[,] matrix)
        {
            int equalSquaresCellsCount = 0;

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    if (matrix[row, col] == matrix[row, col + 1] &&
                        matrix[row, col] == matrix[row + 1, col] &&
                        matrix[row, col] == matrix[row + 1, col + 1])
                    {
                        equalSquaresCellsCount++;
                    }
                }
            }

            return equalSquaresCellsCount;
        }

        private static char[,] InitializeMatrix(int rows, int cols)
        {
            char[,] matrix = new char[rows, cols];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] input = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(char.Parse)
                 .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            return matrix;
        }
    }
}
