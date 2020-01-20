using System;
using System.Linq;

namespace PrimaryDiagonal
{
    class StartUp
    {
        static void Main()
        {
            int squareMatrixSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[squareMatrixSize, squareMatrixSize];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            int diagonalSum = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    diagonalSum += matrix[row, col];
                    row++;
                }
            }

            Console.WriteLine(diagonalSum);
        }
    }
}
