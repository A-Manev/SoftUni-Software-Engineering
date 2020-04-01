namespace AquaShop.Models.Aquariums
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AquaShop.Utilities.Messages;
    using AquaShop.Models.Fish.Contracts;
    using AquaShop.Models.Aquariums.Contracts;
    using AquaShop.Models.Decorations.Contracts;

    public abstract class Aquarium : IAquarium
    {
        private List<IDecoration> decorations;
        private List<IFish> fish;

        private string name;

        protected Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
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

        public ICollection<IFish> Fish => this.fish;

        public void AddFish(IFish fish)
        {
            if (this.Capacity - fish.Size < 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            IFish fishToRemove = this.fish.FirstOrDefault(x => x.Name == fish.Name);

            if (fishToRemove == null)
            {
                return false;
            }

            this.fish.Remove(fish);

            return true;
        }

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var currentFish in this.fish)
            {
                currentFish.Eat();
            }
        }

        public abstract string GetInfo();
    }
}
