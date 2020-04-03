namespace SpaceStation.Models.Mission.Contracts
{
    using System.Collections.Generic;

    using Planets.Contracts;
    using Astronauts.Contracts;

    public interface IMission
    {
        void Explore(IPlanet planet, ICollection<IAstronaut> astronauts);
    }
}
