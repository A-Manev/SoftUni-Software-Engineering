namespace MXGP.Models.Races
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MXGP.Utilities.Messages;
    using MXGP.Models.Races.Contracts;
    using MXGP.Models.Riders.Contracts;

    public class Race : IRace
    {
        private const int MINIMIM_NAME_SYMBOLS = 5;
        private const int MINIMIM_LAPS = 1;

        private string name;
        private int laps;
        private List<IRider> riders;

        public Race()
        {
            this.riders = new List<IRider>();
        }

        public Race(string name, int laps)
            : this()
        {
            this.Name = name;
            this.Laps = laps;
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

        public int Laps
        {
            get => this.laps;
            private set
            {
                if (value < MINIMIM_LAPS)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, MINIMIM_LAPS));
                }

                this.laps = value;
            }
        }

        public IReadOnlyCollection<IRider> Riders => this.riders.AsReadOnly();

        public void AddRider(IRider rider)
        {
            if (rider == null)
            {
                throw new ArgumentNullException(ExceptionMessages.RiderInvalid); 
            }

            if (rider.CanParticipate == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RiderNotParticipate, rider.Name));
            }

            if (this.riders.Any(r => r.Name == rider.Name))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.RiderAlreadyAdded, rider.Name, this.Name));
            }

            this.riders.Add(rider);
        }
    }
}
