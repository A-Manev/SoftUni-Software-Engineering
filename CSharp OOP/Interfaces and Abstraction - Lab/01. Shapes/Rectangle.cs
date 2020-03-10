using System;

namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw()
        {
            DrawLine(this.width, '*', '*');

            for (int i = 1; i < this.height - 1; ++i)
            {
                DrawLine(this.width, '*', ' ');
            }

            DrawLine(this.width, '*', '*');
        }

        private void DrawLine(double width, char end, char mid)
        {
            Console.Write(end);

            for (int i = 1; i < width - 1; ++i)
            {
                Console.Write(mid);
            }

            Console.WriteLine(end);
        }

        //public void Draw()
        //{
        //    Console.WriteLine(new string('*', this.Width));

        //    for (int i = 0; i < this.Height - 2; i++)
        //    {
        //        Console.WriteLine("*" + new string(' ', this.Width - 2) + "*");
        //    }

        //    Console.WriteLine(new string('*', this.Width));
        //}
    }
}
