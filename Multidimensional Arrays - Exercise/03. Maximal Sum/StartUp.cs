using System;
using System.Linq;

namespace MaximalSum
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

            int[,] matrix = InitializeMatrix(rows, cols);

            int subMatrixRow = 3;
            int subMatrixCol = 3;
            int maxSum = int.MinValue;
            int startRow = 0;
            int startCol = 0;

            for (int row = 0; row < matrix.GetLength(0) - 2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 2; col++)
                {
                    int matrixSum = 0;

                    for (int subRow = 0; subRow < subMatrixRow; subRow++)
                    {
                        for (int subCol = 0; subCol < subMatrixCol; subCol++)
                        {
                            matrixSum += matrix[row + subRow, col + subCol];
                        }
                    }

                    if (matrixSum > maxSum)
                    {
                        maxSum = matrixSum;
                        startRow = row;
                        startCol = col;
                    }
                }
            }

            Console.WriteLine("Sum = " + maxSum);

            for (int row = 0; row < subMatrixRow; row++)
            {
                for (int col = 0; col < subMatrixCol; col++)
                {
                    Console.Write(matrix[row + startRow, col + startCol] + " ");
                }

                Console.WriteLine();
            }

        }

        private static int[,] InitializeMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];

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

            return matrix;
        }
    }
}
