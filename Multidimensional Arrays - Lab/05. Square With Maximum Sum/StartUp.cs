using System;
using System.Linq;

namespace SquareWithMaximumSum
{
    class StartUp
    {
        static void Main()
        {
            int[] dimensions = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int rows = dimensions[0];
            int cols = dimensions[1];

            int[,] matrix = new int[rows, cols];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] input = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            int subMatrixRow = 2;
            int subMatrixCol = 2;
            int maxSum = int.MinValue;
            int maxSumRow = 0;
            int maxSumCol = 0;

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    int subMatrixSum = 0;

                    for (int subRow = 0; subRow < subMatrixRow; subRow++)
                    {
                        for (int subCol = 0; subCol < subMatrixCol; subCol++)
                        {
                            subMatrixSum += matrix[row + subRow, col + subCol];
                        }
                    }

                    if (subMatrixSum > maxSum)
                    {
                        maxSum = subMatrixSum;
                        maxSumRow = row;
                        maxSumCol = col;
                    }
                }
            }

            for (int row = 0; row < subMatrixRow; row++)
            {
                for (int col = 0; col < subMatrixCol; col++)
                {
                    Console.Write(matrix[maxSumRow + row, maxSumCol + col] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine(maxSum);
        }
    }
}
