using System;

namespace ReVolt
{
    public class StartUp
    {
        static char[][] matrix;
        static int playerRow;
        static int playerCol;

        static string command;
        static int countAllComands;

        static int backRow;
        static int backCol;

        public static void Main()
        {
            int size = int.Parse(Console.ReadLine());
            int commandsCount = int.Parse(Console.ReadLine());

            countAllComands = commandsCount;

            matrix = new char[size][];

            FillMatrix();

            for (int i = 0; i < commandsCount; i++)
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case "up": Move(-1, 0); break;
                    case "down": Move(1, 0); break;
                    case "left": Move(0, -1); break;
                    case "right": Move(0, 1); break;
                }
            }
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                char[] input = Console.ReadLine().ToCharArray();

                matrix[row] = input;

                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'f')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }
        }

        private static void Move(int row, int col)
        {
            if (IsInside(playerRow + row, playerCol + col))
            {
                if (matrix[playerRow][playerCol] == 'f')
                {
                    matrix[playerRow][playerCol] = '-';
                }

                countAllComands--;

                backRow = playerRow;
                backCol = playerCol;

                playerRow += row;
                playerCol += col;

                IsFinish();

                if (countAllComands == 0)
                {
                    Console.WriteLine("Player lost!");
                    matrix[playerRow][playerCol] = 'f';
                    PrintMatrix();
                    Environment.Exit(0);
                }

                if (matrix[playerRow][playerCol] == 'B')
                {
                    if (command == "up")
                    {
                        playerRow -= 1;

                        if (playerRow < 0)
                        {
                            playerRow = matrix.Length - 1;
                        }

                        IsFinish();
                    }
                    else if (command == "down")
                    {
                        playerRow += 1;
                        IsFinish();
                    }
                    else if (command == "left")
                    {
                        playerCol -= 1;

                        if (playerCol < 0)
                        {
                            playerCol = matrix.Length - 1;
                        }

                        IsFinish();
                    }
                    else if (command == "right")
                    {
                        playerCol += 1;
                        IsFinish();
                    }
                }

                if (matrix[playerRow][playerCol] == 'T')
                {
                    playerRow = backRow;
                    playerCol = backCol;
                }
            }
            else
            {
                if (command == "up")
                {
                    playerRow = matrix.Length - 1;
                }
                else if (command == "down")
                {
                    playerRow = 0;
                }
                else if (command == "left")
                {
                    playerCol = matrix.Length - 1;
                }
                else if (command == "right")
                {
                    playerCol = 0;
                }

                countAllComands--;

                if (matrix[playerRow][playerCol] == 'F')
                {
                    Console.WriteLine("Player won!");
                    matrix[playerRow][playerCol] = 'f';
                    PrintMatrix();
                    Environment.Exit(0);
                }

                if (countAllComands == 0)
                {
                    Console.WriteLine("Player lost!");
                    matrix[playerRow][playerCol] = 'f';
                    PrintMatrix();
                    Environment.Exit(0);
                }
            }
        }

        private static void IsFinish()
        {
            if (matrix[playerRow][playerCol] == 'F')
            {
                Console.WriteLine("Player won!");
                matrix[playerRow][playerCol] = 'f';
                PrintMatrix();
                Environment.Exit(0);
            }
        }

        private static void PrintMatrix()
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join("", row));
            }
        }

        public static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.Length &&
                   col >= 0 && col < matrix[row].Length;
        }
    }
}
