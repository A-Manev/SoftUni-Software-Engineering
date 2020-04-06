namespace MXGP.Models.Riders
{
    using System;

    using MXGP.Utilities.Messages;
    using MXGP.Models.Riders.Contracts;
    using MXGP.Models.Motorcycles.Contracts;

    public class Rider : IRider
    {
        private const int MINIMIM_NAME_SYMBOLS = 5;

        private string name;

        public Rider(string name)
        {
            this.Name = name;
        }

        public string Name 
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MINIMIM_NAME_SYMBOLS)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, MINIMIM_NAME_SYMBOLS));
                }

                this.name = value;
            }
        }

        public IMotorcycle Motorcycle { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate { get; private set; }

        public void AddMotorcycle(IMotorcycle motorcycle)
        {
            if (motorcycle == null)
            {
                throw new ArgumentNullException(ExceptionMessages.MotorcycleInvalid);
            }

            this.Motorcycle = motorcycle;

            this.CanParticipate = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
