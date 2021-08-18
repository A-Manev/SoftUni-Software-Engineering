namespace Git.Services
{
    using Git.Data;
    using Git.Models;
    using Git.ViewModels.Commits;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public CommitCreateRepositorieView GetRepositorieInfo(string id)
        {
            return this.db.Repositories
                 .Where(x => x.Id == id)
                 .Select(x => new CommitCreateRepositorieView
                 {
                     RepositoryId = x.Id,
                     RepositoryName = x.Name,
                 }).FirstOrDefault();
        }

        public void Create(CommitInputModel input, string userId)
        {
            var commit = new Commit
            {
                CreatorId = userId,
                CreatedOn = DateTime.UtcNow,
                Description = input.Description,
                RepositoryId = input.Id,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetAll(string userId)
        {
            return this.db.Commits
                 .Where(x => x.CreatorId == userId)
                 .Select(x => new CommitViewModel
                 {
                     Id = x.Id,
                     CreatedOn = x.CreatedOn,
                     Description = x.Description,
                     RepositoryName = x.Repository.Name,
                 })
                 .ToList();
        }

        public void Delete(string id)
        {
            var commit = this.db.Commits.FirstOrDefault(x => x.Id == id);

            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }
    }
}
