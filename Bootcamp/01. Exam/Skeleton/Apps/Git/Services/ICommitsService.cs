namespace Git.Services
{
    using Git.ViewModels.Commits;
    using System.Collections.Generic;

    public interface ICommitsService
    {
        CommitCreateRepositorieView GetRepositorieInfo(string id);

        IEnumerable<CommitViewModel> GetAll(string userId);

        void Create(CommitInputModel inputModel, string userId);

        void Delete(string id);
    }
}
