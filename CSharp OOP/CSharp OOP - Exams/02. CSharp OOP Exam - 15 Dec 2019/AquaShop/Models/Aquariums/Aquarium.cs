namespace AquaShop.Models.Aquariums
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using AquaShop.Utilities.Messages;
    using AquaShop.Models.Fish.Contracts;
    using AquaShop.Models.Aquariums.Contracts;
    using AquaShop.Models.Decorations.Contracts;

    public abstract class Aquarium : IAquarium
    {
        private List<IFish> fishes;
        private List<IDecoration> decorations;

        private string name;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            this.fishes = new List<IFish>();
            this.decorations = new List<IDecoration>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort => this.decorations.Sum(x => x.Comfort);

        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => this.fishes;

        public void AddFish(IFish fish)
        {
            if (this.fishes.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fishes.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            IFish fishToRemove = this.fishes.FirstOrDefault(x => x.Name == fish.Name);

            if (fishToRemove == null)
            {
                return false;
            }

            this.fishes.Remove(fish);

            return true;
        }

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var currentFish in this.fishes)
            {
                currentFish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.Name} ({this.GetType().Name}):");

            string fish = this.Fish.Any()
                ? $"Fish: {string.Join(", ", this.Fish.Select(f => f.Name))}"
                : $"Fish: none";

            stringBuilder.AppendLine(fish);
            stringBuilder.AppendLine($"Decorations: {this.Decorations.Count}");
            stringBuilder.AppendLine($"Comfort: {this.Comfort}");

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
