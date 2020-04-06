namespace MXGP.Repositories
{
    using System.Collections.Generic;

    using MXGP.Repositories.Contracts;

    public abstract class Repository<T> : IRepository<T>
    {
        private List<T> models;

        public Repository()
        {
            this.models = new List<T>();
        }

        public IReadOnlyCollection<T> Models => this.models;

        public void Add(T model)
        {
            this.models.Add(model);
        }

        public bool Remove(T model)
        {
            this.models.Remove(model);

            return true;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return this.models.AsReadOnly();
        }

        public abstract T GetByName(string name);
    }
}
