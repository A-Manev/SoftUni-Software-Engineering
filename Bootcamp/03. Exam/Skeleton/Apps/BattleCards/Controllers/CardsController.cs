namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;

    using SUS.HTTP;
    using SUS.MvcFramework;

    using static Data.DataConstants;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsServic)
        {
            this.cardsService = cardsServic;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Name) || inputModel.Name.Length < CardNameMinLength || inputModel.Name.Length > CardNameMaxLength)
            {
                return this.Error($"Name should be between {CardNameMinLength} and {CardNameMaxLength} characters long!");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Image))
            {
                return this.Error("The image is required!");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Keyword))
            {
                return this.Error("Keyword is required!");
            }

            if (inputModel.Attack < CardAttackMinValue)
            {
                return this.Error("Attack should be non-negative integer!");
            }

            if (inputModel.Health < CardHealthMinValue)
            {
                return this.Error("Health should be non-negative integer!");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Description) || inputModel.Description.Length > CardDescriptionMaxLength)
            {
                return this.Error($"Description is required and its length should be at most {CardDescriptionMaxLength} characters!");
            }

            var userId = this.GetUserId();

            this.cardsService.Create(inputModel, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.cardsService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            var viewModel = this.cardsService.GetUserCollection(userId);

            return this.View(viewModel);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            this.cardsService.AddToollection(cardId, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            this.cardsService.RemoveFromCollection(cardId, userId);

            return this.Redirect("/Cards/Collection");
        }
    }
}
