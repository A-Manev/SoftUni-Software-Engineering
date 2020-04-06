namespace MXGP.Repositories
{
    using System.Linq;

    using MXGP.Models.Motorcycles.Contracts;

    public class MotorcycleRepository : Repository<IMotorcycle>
    {
        public override IMotorcycle GetByName(string name)
        {
            return this.Models.FirstOrDefault(m => m.Model == name);
        }
    }
}
