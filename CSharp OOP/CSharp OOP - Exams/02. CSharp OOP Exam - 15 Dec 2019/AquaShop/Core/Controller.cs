namespace AquaShop.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using AquaShop.Models.Fish;
    using AquaShop.Repositories;
    using AquaShop.Core.Contracts;
    using AquaShop.Models.Aquariums;
    using AquaShop.Models.Decorations;
    using AquaShop.Utilities.Messages;
    using AquaShop.Models.Fish.Contracts;
    using AquaShop.Models.Aquariums.Contracts;
    using AquaShop.Models.Decorations.Contracts;

    public class Controller : IController
    {
        private DecorationRepository decorations;
        private List<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;

            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            this.aquariums.Add(aquarium);

            return string.Format(OutputMessages.SuccessfullyAdded, aquarium.GetType().Name);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;

            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            this.decorations.Add(decoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decoration.GetType().Name);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = this.decorations.Models.FirstOrDefault(d => d.GetType().Name == decorationType);

            if (decoration != null)
            {
                var aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

                aquarium.Decorations.Add(decoration);

                this.decorations.Remove(decoration);

                return string.Format(OutputMessages.EntityAddedToAquarium, decoration.GetType().Name, aquarium.Name);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish;

            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IAquarium currentAquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

            string aquariumType = currentAquarium.GetType().Name;

            bool isTheRightAquariumForTheRightFish =
                fishType == "FreshwaterFish" && aquariumType == "FreshwaterAquarium" ||
                fishType == "SaltwaterFish" && aquariumType == "SaltwaterAquarium";

            if (isTheRightAquariumForTheRightFish)
            {
                currentAquarium.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fish.GetType().Name, currentAquarium.Name);
            }
            else
            {
                return OutputMessages.UnsuitableWater;
            }
        }

        public string FeedFish(string aquariumName)
        {
            int fedCount = 0;

            foreach (var aquarium in this.aquariums)
            {
                if (aquarium.Name == aquariumName)
                {
                    foreach (var fish in aquarium.Fish) 
                    {
                        fish.Eat();
                        fedCount++;
                    }
                }
            }

            return string.Format(OutputMessages.FishFed, fedCount);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

            decimal fishPrice = aquarium.Fish.Select(f => f.Price).Sum();
            decimal decorationPrice = aquarium.Decorations.Select(d => d.Price).Sum();

            decimal totalPrice = fishPrice + decorationPrice;

            return string.Format(OutputMessages.AquariumValue, aquarium.Name, $"{totalPrice:F2}");
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var aquarium in this.aquariums)
            {
                stringBuilder.AppendLine(aquarium.GetInfo());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
