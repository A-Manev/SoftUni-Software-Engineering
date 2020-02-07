using System.Text;

namespace CarSalesman
{
    public class Car
    {
        public Car(string model, Engine engine)
        {
            this.Model = model;
            this.Engine = engine;
        }

        public Car(string model, Engine engine, double weight)
            : this(model, engine)
        {
            this.Weight = weight;
        }

        public Car(string model, Engine engine, string color)
            : this(model, engine)
        {
            this.Color = color;
        }

        public Car(string model, Engine engine, double? weight, string color)
            : this(model, engine)
        {
            this.Weight = weight;
            this.Color = color;
        }

        public string Model { get; set; }

        public Engine Engine { get; set; }

        public double? Weight { get; set; }

        public string Color { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string weight = this.Weight.HasValue
                ? this.Weight.ToString()
                : "n/a";

            string color = string.IsNullOrEmpty(this.Color)
                ? "n/a"
                : this.Color;

            stringBuilder
                .AppendLine($"{this.Model}:")
                .AppendLine($"  {this.Engine}")
                .AppendLine($"  Weight: {weight}")
                .AppendLine($"  Color: {color}");

            return stringBuilder.ToString().TrimEnd(); 
        }
    }
}
