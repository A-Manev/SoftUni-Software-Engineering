namespace Git.Services
{
    using Git.ViewModels.Repositories;

    using System.Collections.Generic;

    public interface IRepositoriesService
    {
        void Create(RepositoryInputModel repositorie, string userId);

        IEnumerable<RepositoryViewModel> GetAll();
    }
}
