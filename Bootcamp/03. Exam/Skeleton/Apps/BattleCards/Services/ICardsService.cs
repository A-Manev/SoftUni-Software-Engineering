namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        void Create(CardInputModel inputModel, string userId);

        IEnumerable<CardViewModel> GetAll();

        IEnumerable<CardViewModel> GetUserCollection(string userId);

        void AddToollection(int cardId, string userId);

        void RemoveFromCollection(int cardId, string userId);
    }
}
