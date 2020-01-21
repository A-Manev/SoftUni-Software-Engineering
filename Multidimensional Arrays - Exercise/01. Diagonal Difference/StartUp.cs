using System;
using System.Linq;

namespace DiagonalDifference
{
    class StartUp
    {
        static void Main()
        {
            int squareMatrixSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[squareMatrixSize, squareMatrixSize];

            FillMatrix(matrix);

            int leftDiagonalSum = LeftDiagonalSum(matrix);

            int rightDiagonalSum = RightDiagonalSum(matrix);

            Console.WriteLine(Math.Abs(leftDiagonalSum - rightDiagonalSum));
        }

        private static void FillMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }

        private static int LeftDiagonalSum(int[,] matrix)
        {
            int leftDiagonalSum = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    leftDiagonalSum += matrix[row, col];
                    row++;
                }
            }

            return leftDiagonalSum;
        }

        private static int RightDiagonalSum(int[,] matrix)
        {
            int rightDiagonalSum = 0;

            for (int row = matrix.GetLength(0) - 1; row >= 0; row--)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    rightDiagonalSum += matrix[row, col];
                    row--;
                }
            }

            return rightDiagonalSum;
        }
    }
}
