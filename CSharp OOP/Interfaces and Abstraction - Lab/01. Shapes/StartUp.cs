using System;

namespace Shapes
{
    public class StartUp
    {
        public static void Main()
        {
            double radius = double.Parse(Console.ReadLine());

            IDrawable circle = new Circle(5);

            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());

            IDrawable rectangle = new Rectangle(5, 10);

            circle.Draw();
            rectangle.Draw();
        }
    }
}
