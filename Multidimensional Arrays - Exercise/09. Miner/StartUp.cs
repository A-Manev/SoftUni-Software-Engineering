using System;
using System.Linq;

namespace Miner
{
    class StartUp
    {
        static int minerRow;
        static int minerCol;
        static int coals;
        static char[,] field;

        static void Main()
        {
            int fieldSize = int.Parse(Console.ReadLine());

            string[] directions = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            field = new char[fieldSize, fieldSize];

            Initializematrix();

            foreach (var currentDirection in directions)
            {
                switch (currentDirection)
                {
                    case "up": Move(-1, 0); break;
                    case "down": Move(1, 0); break;
                    case "left": Move(0, -1); break;
                    case "right": Move(0, 1); break;
                }
            }

            Console.WriteLine($"{coals} coals left. ({minerRow}, {minerCol})");
        }

        private static void Initializematrix()
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                char[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();

                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = input[col];

                    if (field[row, col] == 's')
                    {
                        minerRow = row;
                        minerCol = col;
                    }

                    if (field[row, col] == 'c')
                    {
                        coals++;
                    }
                }
            }
        }

        public static bool IsInside(int row, int col)
        {
            return row >= 0 && row < field.GetLength(0) &&
                   col >= 0 && col < field.GetLength(1);
        }

        private static void Move(int row, int col)
        {
            if (IsInside(minerRow + row, minerCol + col))
            {
                minerRow += row;
                minerCol += col;

                if (field[minerRow, minerCol] == 'e')
                {
                    Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
                    Environment.Exit(0);
                }

                if (field[minerRow,minerCol] == 'c')
                {
                    field[minerRow, minerCol] = '*';
                    coals--;

                    if (coals == 0)
                    {
                        Console.WriteLine($"You collected all coals! ({minerRow}, {minerCol})");
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
