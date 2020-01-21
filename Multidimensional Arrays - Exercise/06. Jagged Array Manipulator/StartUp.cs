using System;
using System.Linq;

namespace JaggedArrayManipulator
{
    class StartUp
    {
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());

            double[][] matrix = InitializeMatrix(rows);

            AnalyzingMatrix(matrix);

            string command = Console.ReadLine();

            while (command != "End")
            {
                string[] cmdArgs = command
                    .Split()
                    .ToArray();

                string operant = cmdArgs[0];
                int row = int.Parse(cmdArgs[1]);
                int col = int.Parse(cmdArgs[2]);
                int value = int.Parse(cmdArgs[3]);

                if (IsInside(matrix, row, col))
                {
                    if (operant == "Add")
                    {
                        matrix[row][col] += value;
                    }
                    else if (operant == "Subtract")
                    {
                        matrix[row][col] -= value;
                    }
                }

                command = Console.ReadLine();
            }

            PrintMatrix(matrix);
        }

        private static double[][] InitializeMatrix(int rows)
        {
            double[][] matrix = new double[rows][];

            for (int i = 0; i < matrix.Length; i++)
            {
                double[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToArray();

                matrix[i] = input;
            }

            return matrix;
        }

        private static void AnalyzingMatrix(double[][] matrix)
        {
            for (int row = 0; row < matrix.Length - 1; row++)
            {
                if (matrix[row].Length == matrix[row + 1].Length)
                {
                    for (int i = 0; i < matrix[row].Length; i++)
                    {
                        matrix[row][i] *= 2;
                        matrix[row + 1][i] *= 2;
                    }
                }
                else
                {
                    for (int i = 0; i < matrix[row].Length; i++)
                    {
                        matrix[row][i] /= 2;
                    }

                    for (int i = 0; i < matrix[row + 1].Length; i++)
                    {
                        matrix[row + 1][i] /= 2;
                    }
                }
            }
        }

        private static void PrintMatrix(double[][] matrix)
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }

        public static bool IsInside(double[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length &&
                   col >= 0 && col < matrix[row].Length;
        }
    }
}
