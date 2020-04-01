namespace AquaShop.Repositories
{
    using System.Linq;
    using System.Collections.Generic;

    using AquaShop.Models.Decorations.Contracts;
    using AquaShop.Repositories.Contracts;

    class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> decorations;

        public DecorationRepository()
        {
            this.decorations = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this.decorations.AsReadOnly();

        public void Add(IDecoration model)
        {
            this.decorations.Add(model);
        }

        public bool Remove(IDecoration model) //CAREE ??????
        {
            IDecoration decoration = this.decorations.FirstOrDefault(d => d.GetType().Name == model.GetType().Name);
           

            if (decoration == null)
            {
                return false;
            }

            this.decorations.Remove(model);

            return true;
        }

        public IDecoration FindByType(string type)
        {
            return this.decorations.FirstOrDefault(d => d.GetType().Name == type);
        }
    }
}
