namespace SpaceStation.Models.Astronauts
{
    using System;

    using SpaceStation.Models.Bags;
    using SpaceStation.Utilities.Messages;
    using SpaceStation.Models.Bags.Contracts;
    using SpaceStation.Models.Astronauts.Contracts;

    public abstract class Astronaut : IAstronaut
    {
        private const int OXYGEN_DECREASE = 10;

        private string name;
        private double oxygen;

        protected Astronaut(string name, double oxygen)
        {
            this.Name = name;
            this.Oxygen = oxygen;
            this.Bag = new Backpack();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }

                this.name = value;
            }
        }

        public double Oxygen
        {
            get => this.oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }

                this.oxygen = value;
            }
        }

        public bool CanBreath => this.Oxygen > 0; 

        public IBag Bag { get; private set; }

        public virtual void Breath()
        {
            if (this.Oxygen - OXYGEN_DECREASE > 0)
            {
                this.Oxygen -= OXYGEN_DECREASE;
            }
            else
            {
                this.Oxygen = 0;
            }
        }
    }
}
