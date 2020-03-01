using System;

namespace Sneaking
{
    public class Engine
    {
        private char[][] matrix;
        private int samRow;
        private int samCol;
        private int enemyRow;
        private int enemyCol;

        public void Run()
        {
            int rows = int.Parse(Console.ReadLine());

            matrix = new char[rows][];

            FillMatrix();

            char[] inputDirections = Console.ReadLine().ToCharArray();

            foreach (var currentDirection in inputDirections)
            {
                switch (currentDirection)
                {
                    case 'U': Move(-1, 0); break;
                    case 'D': Move(1, 0); break;
                    case 'L': Move(0, -1); break;
                    case 'R': Move(0, 1); break;
                    default: MoveEnemies(); break;
                }
            }
        }

        private void FillMatrix()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                char[] input = Console.ReadLine().ToCharArray();

                matrix[row] = input;

                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'S')
                    {
                        samRow = row;
                        samCol = col;
                    }
                }
            }
        }

        private void PrintMatrix()
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join("", row));
            }
        }

        private void Move(int row, int col)
        {
            if (IsInside(samRow + row, samCol + col))
            {
                MoveEnemies();

                FindeEnemyCoordinates();

                if (enemyCol > samCol && matrix[enemyRow][enemyCol] == 'd' && enemyRow == samRow)
                {
                    SamDied();
                }
                else if (enemyCol < samCol && matrix[enemyRow][enemyCol] == 'b' && enemyRow == samRow)
                {
                    SamDied();
                }

                matrix[samRow][samCol] = '.';

                samRow += row;
                samCol += col;

                matrix[samRow][samCol] = 'S';

                FindeEnemyCoordinates();

                if (matrix[enemyRow][enemyCol] == 'N' && samRow == enemyRow)
                {
                    SamKilledNikoladze();
                }
            }
        }

        private void SamKilledNikoladze()
        {
            matrix[enemyRow][enemyCol] = 'X';

            Console.WriteLine("Nikoladze killed!");

            PrintMatrix();

            Environment.Exit(0);
        }

        private void SamDied()
        {
            matrix[samRow][samCol] = 'X';

            Console.WriteLine($"Sam died at {samRow}, {samCol}");

            PrintMatrix();

            Environment.Exit(0);
        }

        private void MoveEnemies()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'b')
                    {
                        matrix[row][col] = '.';

                        if (IsInside(row, col + 1))
                        {
                            matrix[row][col + 1] = 'b';
                            break;
                        }
                        else
                        {
                            matrix[row][col] = 'd';
                            break;
                        }
                    }

                    if (matrix[row][col] == 'd')
                    {
                        matrix[row][col] = '.';

                        if (IsInside(row, col - 1))
                        {
                            matrix[row][col - 1] = 'd';
                            break;
                        }
                        else
                        {
                            matrix[row][col] = 'b';
                            break;
                        }
                    }
                }
            }
        }

        private void FindeEnemyCoordinates()
        {
            for (int currentCol = 0; currentCol < matrix[samRow].Length; currentCol++)
            {
                if (matrix[samRow][currentCol] != '.' && matrix[samRow][currentCol] != 'S')
                {
                    enemyRow = samRow;
                    enemyCol = currentCol;
                }
            }
        }

        private bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.Length &&
                   col >= 0 && col < matrix[row].Length;
        }
    }
}
