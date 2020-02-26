namespace PointInRectangle
{
    public class Rectangle
    {
        public Rectangle(Point topLeft, Point bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public Point TopLeft { get; set; }

        public Point BottomRight { get; set; }

        public bool Contains(Point point)
        {
            bool isXInside = point.X >= this.TopLeft.X && point.X <= this.BottomRight.X;
            bool isYInside = point.Y <= this.TopLeft.Y && point.Y >= this.BottomRight.Y;

            return isXInside && isYInside;
        }
    }
}
