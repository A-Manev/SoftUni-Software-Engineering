namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Cars;

    using SUS.HTTP;
    using SUS.MvcFramework;

    using System;
    using System.Text.RegularExpressions;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public CarsController(ICarsService carsService, IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.View(this.carsService.GetAllWithUnfixedIssues());
            }

            var viewModel = this.carsService.GetAll(userId);

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Redirect("/Cars/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CarInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Redirect("/Cars/All");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Model) || inputModel.Model.Length < 5 || inputModel.Model.Length > 20)
            {
                return this.Error("Car model is required and should be between 5 and 20 characters long.");
            }

            if (inputModel.Year <= 1900 || inputModel.Year > DateTime.Now.Year)
            {
                return this.Error("Invalid car year.");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Image))
            {
                return this.Error("Car image is required.");
            }

            if (!Regex.IsMatch(inputModel.PlateNumber.Trim(), @"^[A-Z]{2}\d{4}[A-Z]{2}$"))
            {
                return this.Error("Invalid plate number. Plate number should be 2 capital English letters, followed by 4 digits, followed by 2 capital English letters.");
            }

            this.carsService.Create(inputModel, userId);

            return this.Redirect("/Cars/All");
        }
    }
}
