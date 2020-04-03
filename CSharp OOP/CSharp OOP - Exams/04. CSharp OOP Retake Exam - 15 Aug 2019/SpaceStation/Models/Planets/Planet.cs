namespace SpaceStation.Models.Planets
{
    using System;
    using System.Collections.Generic;

    using SpaceStation.Utilities.Messages;
    using SpaceStation.Models.Planets.Contracts;

    public class Planet : IPlanet
    {
        private string name;

        private List<string> items;

        public Planet(string name)
        {
            this.Name = name;
            this.items = new List<string>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }

                this.name = value;
            }
        }

        public ICollection<string> Items => this.items.AsReadOnly();

        public void AddItems(string[] planetItems)
        {
            foreach (var item in planetItems)
            {
                this.items.Add(item);
            }
        }

        public void RemoveItem(string item)
        {
            this.items.Remove(item);
        }
    }
}
