﻿namespace Git.Controllers
{
    using Git.Services;
    using Git.ViewModels.Commits;

    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            var viewModel = this.commitsService.GetAll(userId);

            return this.View(viewModel);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.GetRepositorieInfo(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CommitInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length < 5)
            {
                return this.Error("Description is required and should be more than 5 characters.");
            }

            var userId = this.GetUserId();

            this.commitsService.Create(input, userId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.commitsService.Delete(id);

            return this.Redirect("/Commits/All");
        }
    }
}
