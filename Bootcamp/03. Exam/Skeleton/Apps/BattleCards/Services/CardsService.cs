namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.ViewModels.Cards;

    using System.Linq;
    using System.Collections.Generic;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(CardInputModel inputModel, string userId)
        {
            var card = new Card
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.Image,
                Keyword = inputModel.Keyword,
                Attack = inputModel.Attack,
                Health = inputModel.Health,
                Description = inputModel.Description,
            };

            var userCard = new UserCard
            {
                Card = card,
                UserId = userId,
            };

            this.db.Cards.Add(card);
            this.db.UserCards.Add(userCard);
            this.db.SaveChanges();
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return this.db.Cards
                .Select(x => new CardViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Attack = x.Attack,
                    Health = x.Health,
                    Keyword = x.Keyword,
                    Description = x.Description,
                })
                .ToList();
        }

        public IEnumerable<CardViewModel> GetUserCollection(string userId)
        {
            return this.db.Cards
               .Where(x => x.Users.Any(x => x.UserId == userId))
               .Select(x => new CardViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   ImageUrl = x.ImageUrl,
                   Attack = x.Attack,
                   Health = x.Health,
                   Keyword = x.Keyword,
                   Description = x.Description,
               })
               .ToList();
        }

        public void AddToollection(int cardId, string userId)
        {
            var isCardExist = this.db.UserCards.Any(x => x.CardId == cardId && x.UserId == userId);

            if (!isCardExist)
            {
                var userCard = new UserCard
                {
                    CardId = cardId,
                    UserId = userId,
                };

                this.db.UserCards.Add(userCard);
                this.db.SaveChanges();
            }
        }

        public void RemoveFromCollection(int cardId, string userId)
        {
            var userCard = this.db.UserCards
                 .FirstOrDefault(x => x.CardId == cardId && x.UserId == userId);

            this.db.UserCards.Remove(userCard);
            this.db.SaveChanges();
        }
    }
}
