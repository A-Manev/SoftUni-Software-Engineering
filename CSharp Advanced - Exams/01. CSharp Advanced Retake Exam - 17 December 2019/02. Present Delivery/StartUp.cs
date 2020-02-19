using System;
using System.Linq;

namespace PresentDelivery
{
    public class StartUp
    {
        private static char[][] neighbourhood;
        private static int santaRow;
        private static int santaCol;
        private static int happyKids;

        static void Main()
        {
            int presentsCount = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            FillMatrix(n);

            //We should iterate until we recieve end command or we have no presents left
            string direction;
            while ((direction = Console.ReadLine()) != "Christmas morning" && presentsCount > 0)
            {
                //First let's calculate where Santa should go after move
                int nextRow = santaRow;
                int nextCol = santaCol;
                CalculateNextCoordinates(direction, ref nextRow, ref nextCol);
                //Here we assume that we have te correct coordinates

                //We should check the next symbol
                char nextSymbol = neighbourhood[nextRow][nextCol];

                if (nextSymbol == 'V')
                {
                    //If we have a nice kids, we give him a gift and increase count of happy kids
                    presentsCount--;
                    happyKids++;
                }
                else if (nextSymbol == 'C')
                {
                    //Here Santa gets hyped and give gifts to every gift
                    GiveGiftsToAllAround(ref presentsCount, nextRow, nextCol);
                }

                //We should move Santa on the field
                neighbourhood[santaRow][santaCol] = '-';
                neighbourhood[nextRow][nextCol] = 'S';

                //We should not forget to overwrite Santa's Coordinates
                santaRow = nextRow;
                santaCol = nextCol;
            }

            //We must be sure that Santa has ran out of presents
            //We check both the last direction and presents count because there could be a case where we give all presents, we are left with zero presents but we are done so we don't need to print that
            if (direction != "Christmas morning" && presentsCount == 0)
            {
                //Here look at the end of the sentence!!!We use '!' instead of '.' because of wrong description!
                Console.WriteLine("Santa ran out of presents!");
            }

            //We print the state of the field in every case
            PrintMatrix(n);

            //We should calculate how many nice kids('V' signs) there are left on the fiels
            int niceKidsLeftCount = CountOfNiceKidsLeft(n);

            //If there are not nice kids left, then we have that output
            //If there are left nice kids, then job is not done right and have other output
            if (niceKidsLeftCount == 0)
            {
                Console.WriteLine($"Good job, Santa! {happyKids} happy nice kid/s.");
            }
            else
            {
                Console.WriteLine($"No presents for {niceKidsLeftCount} nice kid/s.");
            }
        }

        /// <summary>
        /// Iterates through the field and count nice kids left (symbol 'V')
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int CountOfNiceKidsLeft(int n)
        {
            int niceKidsLeftCount = 0;

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (neighbourhood[row][col] == 'V')
                    {
                        niceKidsLeftCount++;
                    }
                }
            }

            return niceKidsLeftCount;
        }

        /// <summary>
        /// Prints the filed in it's current state
        /// </summary>
        /// <param name="n"></param>
        private static void PrintMatrix(int n)
        {
            for (int row = 0; row < n; row++)
            {
                Console.WriteLine(String.Join(" ", neighbourhood[row]));
            }
        }

        /// <summary>
        /// It takes all kids nearby Santa and gives them a gift
        /// </summary>
        /// <param name="presentsCount"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        private static void GiveGiftsToAllAround(ref int presentsCount, int nextRow, int nextCol)
        {
            //We should count how many gifts we have given
            int countOfGiftsGiven = 0;

            //We should do something only if there is a kid next to Santa's cookie
            if (IsKidOnCoordinates(nextRow, nextCol - 1))
            {
                //Kid to Left
                ProceedCookie(nextRow, nextCol - 1, ref countOfGiftsGiven);
            }

            if (IsKidOnCoordinates(nextRow, nextCol + 1))
            {
                //Kid to Right
                ProceedCookie(nextRow, nextCol + 1, ref countOfGiftsGiven);
            }

            if (IsKidOnCoordinates(nextRow - 1, nextCol))
            {
                //Kid to Up
                ProceedCookie(nextRow - 1, nextCol, ref countOfGiftsGiven);
            }

            if (IsKidOnCoordinates(nextRow + 1, nextCol))
            {
                //Kid to down
                ProceedCookie(nextRow + 1, nextCol, ref countOfGiftsGiven);
            }

            //We remove all given gifts from the all gifts count
            presentsCount -= countOfGiftsGiven;
        }

        /// <summary>
        /// It gives a gift to a kid when Santa has taken a cookie
        /// </summary>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        /// <param name="countOfGiftsGiven"></param>
        private static void ProceedCookie(int nextRow, int nextCol, ref int countOfGiftsGiven)
        {
            //If we have a nice kid, then we have one more happy kid
            //If we have a naughty kid, then we should give him a gift but not increase happy kids count
            if (neighbourhood[nextRow][nextCol] == 'V')
            {
                //We increase happy kids counter
                happyKids++;
            }

            //We remove the kid in both cases
            neighbourhood[nextRow][nextCol] = '-';

            //And we increase the count of given gifts
            countOfGiftsGiven++;
        }

        /// <summary>
        /// Check if there is a kid in the field on the given coordinates
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private static bool IsKidOnCoordinates(int row, int col)
        {
            return neighbourhood[row][col] == 'X' ||
                neighbourhood[row][col] == 'V';
        }

        /// <summary>
        /// Calculates next coordinates after move in given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        private static void CalculateNextCoordinates(string direction, ref int nextRow, ref int nextCol)
        {
            if (direction == "up")
            {
                nextRow--;
            }
            else if (direction == "down")
            {
                nextRow++;
            }
            else if (direction == "left")
            {
                nextCol--;
            }
            else if (direction == "right")
            {
                nextCol++;
            }
        }

        /// <summary>
        /// Reads input and fills the field with the input read. Also it calculates the Santa's coordinates dynamically while reading the input.
        /// </summary>
        /// <param name="n"></param>
        private static void FillMatrix(int n)
        {
            neighbourhood = new char[n][];
            bool santaFound = false;
            for (int row = 0; row < n; row++)
            {
                char[] currentRow = Console.ReadLine()
                    .Split(' ')
                    .Select(char.Parse)
                    .ToArray();

                if (!santaFound)
                {
                    for (int col = 0; col < currentRow.Length; col++)
                    {
                        if (currentRow[col] == 'S')
                        {
                            santaRow = row;
                            santaCol = col;
                            santaFound = true;

                            break;
                        }
                    }
                }

                neighbourhood[row] = currentRow;
            }
        }
    }
}