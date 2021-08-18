namespace Git.ViewModels.Commits
{
    using System;

    public class CommitViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RepositoryName { get; set; }
    }
}
