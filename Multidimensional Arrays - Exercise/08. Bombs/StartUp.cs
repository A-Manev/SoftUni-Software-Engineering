using System;
using System.Linq;

namespace Bombs
{
    class StartUp
    {
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());

            int[][] matrix = InitializeMatrix(rows);

            string[] bombsCells = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            InitializeBombCoordinates(matrix, bombsCells);

            PrintMatrixInfo(matrix);

            PrintMatrix(matrix);
        }

        private static int[][] InitializeMatrix(int rows)
        {
            int[][] matrix = new int[rows][];

            for (int row = 0; row < matrix.Length; row++)
            {
                int[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                matrix[row] = input;
            }

            return matrix;
        }

        private static void InitializeBombCoordinates(int[][] matrix, string[] bombsCells)
        {
            for (int i = 0; i < bombsCells.Length; i++)
            {
                int[] coordinates = bombsCells[i]
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int row = coordinates[0];
                int col = coordinates[1];

                BombCells(matrix, row, col);
            }
        }

        private static void BombCells(int[][] matrix, int row, int col)
        {
            if (matrix[row][col] > 0)
            {
                int bombDamage = matrix[row][col];

                IsInside(matrix, row - 1, col - 1, bombDamage);
                IsInside(matrix, row, col - 1, bombDamage);
                IsInside(matrix, row + 1, col - 1, bombDamage);
                IsInside(matrix, row + 1, col, bombDamage);
                IsInside(matrix, row + 1, col + 1, bombDamage);
                IsInside(matrix, row, col + 1, bombDamage);
                IsInside(matrix, row - 1, col + 1, bombDamage);
                IsInside(matrix, row - 1, col, bombDamage);

                matrix[row][col] = 0;
            }
        }

        public static void IsInside(int[][] matrix, int row, int col, int bombDamage)
        {
            if (row >= 0 && row < matrix.Length &&
                col >= 0 && col < matrix[row].Length
                   && matrix[row][col] > 0)
            {
                matrix[row][col] -= bombDamage;
            }
        }

        private static void PrintMatrixInfo(int[][] matrix)
        {
            var aliveCells = 0;
            int sum = 0;

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] > 0)
                    {
                        aliveCells++;
                        sum += matrix[row][col];
                    }
                }
            }

            Console.WriteLine($"Alive cells: {aliveCells}");

            Console.WriteLine($"Sum: {sum}");
        }

        private static void PrintMatrix(int[][] matrix)
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
