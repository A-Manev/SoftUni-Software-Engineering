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
            if (aquariumType == "FreshwaterAquarium")
            {
                IAquarium aquarium = new FreshwaterAquarium(aquariumName);

                this.aquariums.Add(aquarium);

                return $"Successfully added {aquariumType}.";
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                IAquarium aquarium = new SaltwaterAquarium(aquariumName);

                this.aquariums.Add(aquarium);

                return $"Successfully added {aquariumType}.";
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType == "Ornament")
            {
                Ornament ornament = new Ornament();

                this.decorations.Add(ornament);

                return $"Successfully added {decorationType}.";
            }
            else if (decorationType == "Plant")
            {
                Plant plant = new Plant();

                this.decorations.Add(plant);

                return $"Successfully added {decorationType}.";
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = this.decorations.Models.FirstOrDefault(d => d.GetType().Name == decorationType);

            if (decoration != null)
            {
                var aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName); // care does aquarium exist 

                aquarium.Decorations.Add(decoration);

                this.decorations.Remove(decoration);

                return $"Successfully added {decorationType} to {aquariumName}.";
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType == "FreshwaterFish")
            {
                IAquarium aquarium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

                if (aquarium.GetType().Name != "FreshwaterAquarium")
                {
                    return "Water not suitable.";
                }
                else
                {
                    IFish freshwaterFish = new FreshwaterFish(fishName, fishSpecies, price);

                    aquarium.Fish.Add(freshwaterFish);

                    return $"Successfully added {fishType} to {aquariumName}.";
                }
            }
            else if (fishType == "SaltwaterFish")
            {
                IAquarium aquarium = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

                if (aquarium.GetType().Name != "SaltwaterAquarium")
                {
                    return "Water not suitable.";
                }
                else
                {
                    IFish SaltwaterFish = new SaltwaterFish(fishName, fishSpecies, price);

                    aquarium.Fish.Add(SaltwaterFish);

                    return $"Successfully added {fishType} to {aquariumName}.";
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }
        }

        public string FeedFish(string aquariumName)
        {
            int fedCount = 0; 

            foreach (var aquarium in this.aquariums)
            {
                if (aquarium.Name == aquariumName)
                {
                    foreach (var fish in aquarium.Fish) // care
                    {
                        fish.Eat();
                        fedCount++;
                    }
                }
            }

            return $"Fish fed: {fedCount}";
        }

        public string CalculateValue(string aquariumName)
        {
            decimal value = 0;

            IAquarium aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

            foreach (var fish in aquarium.Fish)
            {
                value += fish.Price;
            }

            foreach (var decoration in aquarium.Decorations)
            {
                value += decoration.Price;
            }

            return $"The value of Aquarium {aquariumName} is {value:F2}.";
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
