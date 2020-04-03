namespace SpaceStation.Models.Planets.Contracts
{
    using System.Collections.Generic;

    public interface IPlanet
    {
        string Name { get; }

        ICollection<string> Items { get; }

        void AddItems(string[] planetItems);

        void RemoveItem(string item);
    }
}