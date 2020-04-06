namespace MXGP.Repositories
{
    using System.Linq;

    using MXGP.Models.Races.Contracts;

    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name)
        {
            return this.Models.FirstOrDefault(m => m.Name == name);
        }
    }
}
