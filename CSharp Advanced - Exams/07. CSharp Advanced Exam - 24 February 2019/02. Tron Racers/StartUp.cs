using System;
using System.Linq;

namespace TronRacers
{
    public class StartUp
    {
        static char[][] matrix;
        static int firstPlayerRow;
        static int firstPlayerCol;
        static int secondPlayerRow;
        static int secondPlayerCol;
        static string firstPlayerDirection;
        static string secondPlayerDirection;

        public static void Main()
        {
            int rows = int.Parse(Console.ReadLine());

            matrix = new char[rows][];

            for (int row = 0; row < matrix.Length; row++)
            {
                char[] input = Console.ReadLine().ToCharArray();

                matrix[row] = input;

                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'f')
                    {
                        firstPlayerRow = row;
                        firstPlayerCol = col;
                    }

                    if (matrix[row][col] == 's')
                    {
                        secondPlayerRow = row;
                        secondPlayerCol = col;
                    }
                }
            }

            while (true)
            {
                string[] command = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                firstPlayerDirection = command[0];
                secondPlayerDirection = command[1];

                switch (firstPlayerDirection)
                {
                    case "up": MoveFirst(-1, 0); break;
                    case "down": MoveFirst(1, 0); break;
                    case "left": MoveFirst(0, -1); break;
                    case "right": MoveFirst(0, 1); break;
                }

                switch (secondPlayerDirection)
                {
                    case "up": MoveSecond(-1, 0); break;
                    case "down": MoveSecond(1, 0); break;
                    case "left": MoveSecond(0, -1); break;
                    case "right": MoveSecond(0, 1); break;
                }
            }
        }

        private static void MoveFirst(int row, int col)
        {
            if (IsInside(firstPlayerRow + row, firstPlayerCol + col))
            {
                firstPlayerRow += row;
                firstPlayerCol += col;

                FirstPlayerWrongMove();
            }
            else
            {
                if (firstPlayerDirection == "up")
                {
                    firstPlayerRow = matrix.Length - 1;

                    FirstPlayerWrongMove();
                }
                else if (firstPlayerDirection == "down")
                {
                    firstPlayerRow = 0;

                    FirstPlayerWrongMove();
                }
                else if (firstPlayerDirection == "left")
                {
                    firstPlayerCol = matrix.Length - 1;

                    FirstPlayerWrongMove();
                }
                else if (firstPlayerDirection == "right")
                {
                    firstPlayerCol = 0;

                    FirstPlayerWrongMove();
                }
            }
        } 

        private static void MoveSecond(int row, int col)
        {
            if (IsInside(secondPlayerRow + row, secondPlayerCol + col))
            {
                secondPlayerRow += row;
                secondPlayerCol += col;

                SecondPlayerWrongmove();
            }
            else
            {
                if (secondPlayerDirection == "up")
                {
                    secondPlayerRow = matrix.Length - 1;

                    SecondPlayerWrongmove();
                }
                else if (secondPlayerDirection == "down")
                {
                    secondPlayerRow = 0;

                    SecondPlayerWrongmove();
                }
                else if (secondPlayerDirection == "left")
                {
                    secondPlayerCol = matrix.Length - 1;

                    SecondPlayerWrongmove();
                }
                else if (secondPlayerDirection == "right")
                {
                    secondPlayerCol = 0;

                    SecondPlayerWrongmove();
                }
            }
        }

        private static void SecondPlayerWrongmove()
        {
            if (matrix[secondPlayerRow][secondPlayerCol] == 'f')
            {
                matrix[secondPlayerRow][secondPlayerCol] = 'x';
                PrintMatrix();
                Environment.Exit(0);
            }
            else
            {
                matrix[secondPlayerRow][secondPlayerCol] = 's';
            }
        }

        private static void FirstPlayerWrongMove()
        {
            if (matrix[firstPlayerRow][firstPlayerCol] == 's')
            {
                matrix[firstPlayerRow][firstPlayerCol] = 'x';
                PrintMatrix();
                Environment.Exit(0);
            }
            else
            {
                matrix[firstPlayerRow][firstPlayerCol] = 'f';
            }
        }

        public static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.Length &&
                   col >= 0 && col < matrix[row].Length;
        }

        private static void PrintMatrix()
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join("", row));
            }
        }
    }
}
