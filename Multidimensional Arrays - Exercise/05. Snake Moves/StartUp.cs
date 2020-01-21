using System;
using System.Linq;

namespace SnakeMoves
{
    class StartUp
    {
        static void Main()
        {
            int[] dimensions = Console.ReadLine()
                  .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                  .Select(int.Parse)
                  .ToArray();

            string snake = Console.ReadLine();

            int rows = dimensions[0];
            int cols = dimensions[1];

            char[,] matrix = InitializeSnake(snake, rows, cols);

            PrintSnake(matrix);
        }

        private static char[,] InitializeSnake(string snake, int rows, int cols)
        {
            char[,] matrix = new char[rows, cols];

            int count = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (count < snake.Length)
                    {
                        matrix[row, col] = snake[count];
                        count++;
                    }
                    else
                    {
                        count = 0;
                        matrix[row, col] = snake[count];
                        count++;
                    }
                }
            }

            return matrix;
        }

        private static void PrintSnake(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                if (row % 2 == 0)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        Console.Write(matrix[row, col]);
                    }

                    Console.WriteLine();
                }
                else
                {
                    for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
                    {
                        Console.Write(matrix[row, col]);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
