namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Users;

    using SUS.HTTP;
    using SUS.MvcFramework;

    using System.ComponentModel.DataAnnotations;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel inputModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId == null)
            {
                return this.Error("Invalid username or password.");
            }

            this.SignIn(userId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Username) || inputModel.Username.Length < 4 || inputModel.Username.Length > 20)
            {
                return this.Error("Invalid username. The username should be between 4 and 20 characters.");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Email) || !new EmailAddressAttribute().IsValid(inputModel.Email))
            {
                return this.Error("Invalid email.");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Password) || inputModel.Password.Length < 5 || inputModel.Password.Length > 20)
            {
                return this.Error("Invalid password. The password should be between 5 and 20 characters.");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            if (!this.usersService.IsUsernameAvailable(inputModel.Username))
            {
                return this.Error("Username already taken.");
            }

            if (!this.usersService.IsEmailAvailable(inputModel.Email))
            {
                return this.Error("Email already taken.");
            }

            //if (string.IsNullOrWhiteSpace(inputModel.userType) || inputModel.userType != "Mechanic" || inputModel.userType != "Client") 
            //{
            //    return this.Error("Invalid user type.");
            //}

            this.usersService.Create(inputModel.Username, inputModel.Email, inputModel.Password, inputModel.userType);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged-in users can logout.");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
