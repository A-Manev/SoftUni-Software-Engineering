namespace SpaceStation.Models.Bags
{
    using System.Collections.Generic;

    using SpaceStation.Models.Bags.Contracts;

    public class Backpack : IBag
    {
        private List<string> items;

        public Backpack()
        {
            this.items = new List<string>();
        }

        public ICollection<string> Items => this.items.AsReadOnly();

        public void AddItem(string item)
        {
            this.items.Add(item);
        }
    }
}
