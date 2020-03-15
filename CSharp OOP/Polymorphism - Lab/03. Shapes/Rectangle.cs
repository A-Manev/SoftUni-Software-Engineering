namespace Shapes
{
    public class Rectangle : Shape
    {
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }

        public double Height
        {
            get => this.height;
            set => this.height = value;
        }

        public double Width 
        {
            get => this.width;
            set => this.width = value; 
        }

        public override double CalculateArea()
        {
            return this.Height * this.Width;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (this.Height + this.Width);
        }

        public override string Draw()
        {
            return base.Draw() + this.GetType().Name;
        }
    }
}
