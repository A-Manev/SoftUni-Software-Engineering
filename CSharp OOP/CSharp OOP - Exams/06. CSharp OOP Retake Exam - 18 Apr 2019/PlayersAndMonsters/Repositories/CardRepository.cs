namespace PlayersAndMonsters.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using PlayersAndMonsters.Utilities.Messages;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Repositories.Contracts;

    public class CardRepository : ICardRepository
    {
        private List<ICard> cards;

        public CardRepository()
        {
            this.cards = new List<ICard>();
        }

        public int Count => this.Cards.Count;

        public IReadOnlyCollection<ICard> Cards => this.cards.AsReadOnly();

        public void Add(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException(ExceptionMessages.NullCard);
            }

            if (this.cards.Any(c => c.Name == card.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingCard, card.Name));
            }

            this.cards.Add(card);
        }

        public bool Remove(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException(ExceptionMessages.NullCard);
            }

            if (!this.cards.Any(c => c.Name == card.Name)) // CARE HERE
            {
                return false;
            }

            this.cards.Remove(card);

            return true;
        }

        public ICard Find(string name)
        {
            return this.cards.FirstOrDefault(c => c.Name == name);
        }
    }
}
