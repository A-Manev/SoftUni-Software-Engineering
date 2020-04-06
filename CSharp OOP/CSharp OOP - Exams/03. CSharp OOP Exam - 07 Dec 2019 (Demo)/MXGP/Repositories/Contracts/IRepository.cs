namespace MXGP.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void Add(T model);

        bool Remove(T model);

        T GetByName(string name);

        IReadOnlyCollection<T> GetAll();
    }
}
