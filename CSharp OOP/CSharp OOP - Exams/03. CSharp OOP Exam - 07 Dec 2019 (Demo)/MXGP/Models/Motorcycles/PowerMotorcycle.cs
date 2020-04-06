namespace MXGP.Models
{
    using System;

    using MXGP.Models.Motorcycles;
    using MXGP.Utilities.Messages;

    public class PowerMotorcycle : Motorcycle
    {
        private const double DEFAULT_CUBIC_CENTIMETERS = 450;
        private const int MINIMUM_HORSEPOWER = 70;
        private const int MAXIMUM_HORSEPOWER = 100;

        private int horsePower;

        public PowerMotorcycle(string model, int horsePower)
            : base(model, horsePower, DEFAULT_CUBIC_CENTIMETERS)
        {

        }

        public override int HorsePower 
        { 
            get => this.horsePower; 
            protected set
            {
                if (value < MINIMUM_HORSEPOWER || value > MAXIMUM_HORSEPOWER)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }

                this.horsePower = value;
            }
        }
    }
}
