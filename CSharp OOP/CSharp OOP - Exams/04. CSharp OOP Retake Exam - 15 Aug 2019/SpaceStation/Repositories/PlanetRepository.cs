namespace SpaceStation.Repositories
{
    using System.Linq;
    using System.Collections.Generic;

    using SpaceStation.Repositories.Contracts;
    using SpaceStation.Models.Planets.Contracts;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets.AsReadOnly();

        public void Add(IPlanet model)
        {
            this.planets.Add(model);
        }

        public bool Remove(IPlanet model)
        {
            if (!this.planets.Any(p => p.Name == model.Name))
            {
                return false;
            }

            this.planets.Remove(model);

            return true;
        }

        public IPlanet FindByName(string name)
        {
            return this.planets.FirstOrDefault(p => p.Name == name);
        }
    }
}
