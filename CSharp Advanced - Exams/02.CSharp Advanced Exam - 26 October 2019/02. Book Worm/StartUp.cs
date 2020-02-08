using System;
using System.Text;

namespace BookWorm
{
    class StartUp
    {
        static char[,] matrix;
        static int wormRow;
        static int wormCol;
        static StringBuilder @string;

        public static void Main()
        {
            @string = new StringBuilder();

            string initialString = Console.ReadLine();

            int squareShapeSize = int.Parse(Console.ReadLine());

            matrix = new char[squareShapeSize, squareShapeSize];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string input = Console.ReadLine();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];

                    if (matrix[row, col] == 'P')
                    {
                        wormRow = row;
                        wormCol = col;
                    }
                }
            }

            @string.Append(initialString);

            string command = Console.ReadLine();

            while (command != "end")
            {
                switch (command)
                {
                    case "up": Move(-1, 0); break;
                    case "down": Move(1, 0); break;
                    case "left": Move(0, -1); break;
                    case "right": Move(0, 1); break;
                }

                command = Console.ReadLine();
            }

            PrintMatrix();
        }

        private static void PrintMatrix()
        {
            Console.WriteLine(@string.ToString());
            matrix[wormRow, wormCol] = 'P';

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private static void Move(int row, int col)
        {
            if (IsInside(wormRow + row, wormCol + col))
            {
                if (matrix[wormRow, wormCol] == 'P')
                {
                    matrix[wormRow, wormCol] = '-';
                }

                wormRow += row;
                wormCol += col;

                char cell = matrix[wormRow, wormCol];

                if (char.IsLetter(cell))
                {
                    @string.Append(cell);
                    matrix[wormRow, wormCol] = '-';
                }

                matrix[wormRow, wormCol] = 'P';
            }
            else
            {
                @string.Remove(@string.Length - 1, 1);
            }
        }

        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) &&
                   col >= 0 && col < matrix.GetLength(1);
        }
    }
}
