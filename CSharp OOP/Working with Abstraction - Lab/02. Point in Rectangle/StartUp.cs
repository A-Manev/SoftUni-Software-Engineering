using System;
using System.Linq;

namespace PointInRectangle
{
    public class StartUp
    {
        public static void Main()
        {
            int[] coordinates = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int topLeftX = coordinates[0];
            int topLeftY = coordinates[1];
            int bottomRightX = coordinates[2];
            int bottomRightY = coordinates[3];

            Rectangle rectangle = new Rectangle(
                new Point(topLeftX, bottomRightY), 
                new Point(bottomRightX, topLeftY));

            int numberOfPoints = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfPoints; i++)
            {
                int[] input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

                int x = input[0];
                int y = input[1];

                Point point = new Point(x, y);

                Console.WriteLine(rectangle.Contains(point));
            }
        }
    }
}
