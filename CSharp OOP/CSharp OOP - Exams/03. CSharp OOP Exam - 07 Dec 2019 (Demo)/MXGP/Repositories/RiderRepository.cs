namespace MXGP.Repositories
{
    using System.Linq;

    using MXGP.Models.Riders.Contracts;

    public class RiderRepository : Repository<IRider>
    {
        public override IRider GetByName(string name)
        {
            return this.Models.FirstOrDefault(m => m.Name == name);
        }
    }
}
