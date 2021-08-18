namespace Git.Controllers
{
    using Git.Services;
    using Git.ViewModels.Repositories;

    using SUS.HTTP;
    using SUS.MvcFramework;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var viewModel = this.repositoriesService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Name.Length < 3 || input.Name.Length > 10)
            {
                return this.Error("Invalid Repository name. The name should be between 3 and 10 characters.");
            }

            if (string.IsNullOrEmpty(input.RepositoryType))
            {
                return this.Error("Repository type is required.");
            }

            if (input.RepositoryType != "Public" && input.RepositoryType != "Private")
            {
                return this.Error("Invalid repository type.");
            }

            var userId = this.GetUserId();

            this.repositoriesService.Create(input, userId);

            return this.Redirect("/Repositories/All");
        }
    }
}
