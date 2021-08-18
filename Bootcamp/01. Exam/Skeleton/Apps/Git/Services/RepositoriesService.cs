namespace Git.Services
{
    using Git.Data;
    using Git.Models;
    using Git.ViewModels.Repositories;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(RepositoryInputModel input, string userId)
        {
            var repository = new Repository
            {
                Name = input.Name,
                OwnerId = userId,
                IsPublic = false,
                CreatedOn = DateTime.UtcNow,
            };

            if (input.RepositoryType == "Public")
            {
                repository.IsPublic = true;
            }

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            return this.db.Repositories
                 .Where(x => x.IsPublic == true)
                 .Select(x => new RepositoryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Owner = x.Owner.Username,
                     CreatedOn = x.CreatedOn,
                     CommitsCount = x.Commits.Count,
                 })
                 .ToList();
        }
    }
}
