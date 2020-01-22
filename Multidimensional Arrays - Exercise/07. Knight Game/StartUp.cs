using System;

namespace KnightGame
{
    class StartUp
    {
        static int currentKnightEnemies;

        static void Main()
        {
            int bordSize = int.Parse(Console.ReadLine());

            char[][] chessBord = InitializeChessBord(bordSize);

            int removedKnights = 0;

            while (true)
            {
                currentKnightEnemies = 0;
                int maxAttacks = int.MinValue;
                int killerRow = 0;
                int killerCol = 0;

                for (int row = 0; row < chessBord.Length; row++)
                {
                    for (int col = 0; col < chessBord[row].Length; col++)
                    {
                        if (chessBord[row][col] == 'K')
                        {
                            BordCells(chessBord, row, col);

                            if (currentKnightEnemies > maxAttacks)
                            {
                                maxAttacks = currentKnightEnemies;
                                killerRow = row;
                                killerCol = col;
                            }

                            currentKnightEnemies = 0;
                        }
                    }
                }

                if (maxAttacks > 0)
                {
                    chessBord[killerRow][killerCol] = '0';
                    removedKnights++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(removedKnights);
        }

        private static void BordCells(char[][] chessBord, int row, int col)
        {
            IsInside(chessBord, row - 2, col - 1);
            IsInside(chessBord, row - 2, col + 1);
            IsInside(chessBord, row + 2, col - 1);
            IsInside(chessBord, row + 2, col + 1);
            IsInside(chessBord, row - 1, col - 2);
            IsInside(chessBord, row + 1, col - 2);
            IsInside(chessBord, row - 1, col + 2);
            IsInside(chessBord, row + 1, col + 2);
        }

        private static char[][] InitializeChessBord(int bordSize)
        {
            char[][] chessBord = new char[bordSize][];

            for (int row = 0; row < chessBord.Length; row++)
            {
                string input = Console.ReadLine();

                char[] line = input.ToCharArray();

                chessBord[row] = line;
            }

            return chessBord;
        }

        public static void IsInside(char[][] chessBord, int row, int col)
        {
            if (row >= 0 && row < chessBord.Length &&
                   col >= 0 && col < chessBord[row].Length
                   && chessBord[row][col] == 'K')
            {
                currentKnightEnemies++;
            }
        }
    }
}
