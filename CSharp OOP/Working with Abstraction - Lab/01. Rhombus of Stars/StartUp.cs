using System;
using System.Text;

namespace RhombusOfStars
{
    public class StartUp
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            RhombusAsStringDrawer rhombusDrawer = new RhombusAsStringDrawer();

            string rhombusAsString = rhombusDrawer.Draw(n);

            Console.WriteLine(rhombusAsString);
        }
    }

    public class RhombusAsStringDrawer
    {
        public string Draw(int countOfStarts)
        {
            StringBuilder stringBuilder = new StringBuilder();

            DrawTopPart(stringBuilder, countOfStarts);
            DrawMiddlePart(stringBuilder, countOfStarts);
            DrawBottomPart(stringBuilder, countOfStarts);

            return stringBuilder.ToString().TrimEnd();
        }

        private void DrawTopPart(StringBuilder stringBuilder, int n)
        {
            for (int star = 1; star < n; star++)
            {
                stringBuilder.Append(new string(' ', n - star));

                DrawLineOfStars(stringBuilder, star);
            }
        }

        private void DrawMiddlePart(StringBuilder stringBuilder, int n)
        {
            DrawLineOfStars(stringBuilder, n);
        }

        private void DrawBottomPart(StringBuilder stringBuilder, int n)
        {
            for (int star = n - 1; star >= 1; star--)
            {
                stringBuilder.Append(new string(' ', n - star));

                DrawLineOfStars(stringBuilder, star);
            }
        }

        private static void DrawLineOfStars(StringBuilder stringBuilder, int numberOfStars)
        {
            for (int star = 0; star < numberOfStars; star++)
            {
                stringBuilder.Append("*");

                if (star < numberOfStars - 1)
                {
                    stringBuilder.Append(" ");
                }
            }

            stringBuilder.AppendLine();
        }
    }
}
