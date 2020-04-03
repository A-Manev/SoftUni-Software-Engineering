namespace SpaceStation.Repositories
{
    using System.Linq;
    using System.Collections.Generic;

    using SpaceStation.Repositories.Contracts;
    using SpaceStation.Models.Astronauts.Contracts;

    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> spaceStation;

        public AstronautRepository()
        {
            this.spaceStation = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => this.spaceStation.AsReadOnly();

        public void Add(IAstronaut model)
        {
            this.spaceStation.Add(model);
        }

        public bool Remove(IAstronaut model)
        {
            if (!this.spaceStation.Any(a => a.Name == model.Name))
            {
                return false;
            }

            this.spaceStation.Remove(model);

            return true;
        }

        public IAstronaut FindByName(string name)
        {
            return this.spaceStation.FirstOrDefault(a => a.Name == name);
        }
    }
}
