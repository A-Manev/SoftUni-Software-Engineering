namespace MXGP.Models.Motorcycles
{
    using System;

    using MXGP.Utilities.Messages;
    using MXGP.Models.Motorcycles.Contracts;

    public abstract class Motorcycle : IMotorcycle
    {
        private const int MINIMUM_MODEL_SYMBOLS = 4;

        private string model;

        protected Motorcycle(string model, int horsePower, double cubicCentimeters)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MINIMUM_MODEL_SYMBOLS)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, MINIMUM_MODEL_SYMBOLS));
                }

                this.model = value;
            }
        }

        public virtual int HorsePower { get; protected set; }

        public double CubicCentimeters { get; private set; }

        public double CalculateRacePoints(int laps)
        {
            return this.CubicCentimeters / this.HorsePower * laps;
        }
    }
}
