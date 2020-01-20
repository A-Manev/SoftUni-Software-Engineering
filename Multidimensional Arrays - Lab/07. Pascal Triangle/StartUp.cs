using System;

namespace PascalTriangle
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            long[][] pascalTriangle = new long[n][];

            for (int i = 0; i < pascalTriangle.Length; i++)
            {
                pascalTriangle[i] = new long[i + 1];
            }
            
            for (int row = 0; row < pascalTriangle.Length; row++)
            {
                pascalTriangle[row][0] = 1;
                pascalTriangle[row][pascalTriangle[row].Length - 1] = 1;

                for (int col = 1; col < pascalTriangle[row].Length - 1; col++)
                {
                    pascalTriangle[row][col] =
                        pascalTriangle[row - 1][col - 1] +
                        pascalTriangle[row - 1][col];
                }
            }

            foreach (var row in pascalTriangle)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
