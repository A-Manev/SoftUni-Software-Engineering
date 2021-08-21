namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using CarShop.ViewModels.Issues;

    using SUS.HTTP;
    using SUS.MvcFramework;

    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;

        public IssuesController(IIssuesService issuesService, IUsersService usersService)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
        }

        public HttpResponse Add(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CarIdViewModel
            {
                Id = carId
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Add(IssueInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Description) || inputModel.Description.Length < 5)
            {
                return this.Error("Issue description is required and should be more then 5 symbols.");
            }

            this.issuesService.Create(inputModel);

            return this.Redirect($"/Issues/CarIssues?carId={inputModel.carId}");
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.issuesService.GetAll(carId);

            return this.View(viewModel);
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                this.issuesService.Fix(issueId);
            }

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.issuesService.Delete(issueId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
