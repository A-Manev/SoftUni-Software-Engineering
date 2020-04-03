namespace SpaceStation.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Collections.Generic;

    using SpaceStation.Repositories;
    using SpaceStation.Core.Contracts;
    using SpaceStation.Models.Mission;
    using SpaceStation.Models.Planets;
    using SpaceStation.Models.Astronauts;
    using SpaceStation.Utilities.Messages;
    using SpaceStation.Models.Planets.Contracts;
    using SpaceStation.Models.Mission.Contracts;
    using SpaceStation.Models.Astronauts.Contracts;

    public class Controller : IController
    {
        private PlanetRepository planetRepository;
        private AstronautRepository astronautRepository;

        private IMission mission;
        private int exploredPlanetsCount;

        public Controller()
        {
            this.mission = new Mission();
            this.planetRepository = new PlanetRepository();
            this.astronautRepository = new AstronautRepository();
        }

        public string AddAstronaut(string astronautType, string astronautName)
        {
            IAstronaut astronaut;

            switch (astronautType)
            {
                case "Biologist":
                    astronaut = new Biologist(astronautName);
                    break;

                case "Geodesist":
                    astronaut = new Geodesist(astronautName);
                    break;

                case "Meteorologist":
                    astronaut = new Meteorologist(astronautName);
                    break;

                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            //Assembly assembly = Assembly.GetExecutingAssembly();

            //var type = assembly
            //    .GetTypes()
            //    .FirstOrDefault(t => t.Name == astronautType);

            //if (type == null)
            //{
            //    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            //}

            //var astronaut = (IAstronaut)Activator.CreateInstance(type, astronautName);

            this.astronautRepository.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded,
                astronaut.GetType().Name,
                astronaut.Name);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            planet.AddItems(items);

            this.planetRepository.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planet.Name);
        }

        public string RetireAstronaut(string astronautName)
        {
            var targetAstronaut = this.astronautRepository.FindByName(astronautName);

            if (this.astronautRepository.FindByName(astronautName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            this.astronautRepository.Remove(targetAstronaut);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> suitableAstronauts = this.astronautRepository
                .Models
                .Where(a => a.Oxygen > 60)
                .ToList();

            if (suitableAstronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planet = this.planetRepository.FindByName(planetName);

            this.mission.Explore(planet, suitableAstronauts);

            this.exploredPlanetsCount++;

            int deadAstronauts = 0;

            foreach (var astronaut in suitableAstronauts)
            {
                if (astronaut.CanBreath == false)
                {
                    deadAstronauts++;
                }
            }

            return string.Format(OutputMessages.PlanetExplored, planet.Name, deadAstronauts);
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.exploredPlanetsCount} planets were explored!");
            stringBuilder.AppendLine($"Astronauts info:");

            foreach (var astronaut in this.astronautRepository.Models)
            {
                stringBuilder.AppendLine($"Name: {astronaut.Name}");
                stringBuilder.AppendLine($"Oxygen: {astronaut.Oxygen}");

                string astronautBagitems = astronaut.Bag.Items.Count == 0
                    ? "none"
                    : string.Join(", ", astronaut.Bag.Items);

                stringBuilder.AppendLine($"Bag items: {astronautBagitems}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
