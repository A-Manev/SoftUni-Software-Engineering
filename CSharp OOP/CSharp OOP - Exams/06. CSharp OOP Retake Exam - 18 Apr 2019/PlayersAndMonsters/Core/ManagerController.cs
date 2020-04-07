namespace PlayersAndMonsters.Core
{
    using System;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Players;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories;
    using PlayersAndMonsters.Repositories.Contracts;
    using PlayersAndMonsters.Utilities.Messages;

    public class ManagerController : IManagerController
    {
        private PlayerRepository playerRepository;
        private ICardRepository cardRepository;

        public ManagerController()
        {
            this.playerRepository = new PlayerRepository();
            this.cardRepository = new CardRepository();
        }

        public string AddPlayer(string type, string username)
        {
            IPlayer player = null;

            if (type == "Beginner")
            {
                player = new Beginner(new CardRepository(), username);
            }
            else if (type == "Advanced")
            {
                player = new Advanced(new CardRepository(), username);
            }

            this.playerRepository.Add(player);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, type, username);
        }

        public string AddCard(string type, string name)
        {
            ICard card = null;

            if (type == "Trap")
            {
                card = new TrapCard(name);
            }
            else if (type == "Magic")
            {
                card = new MagicCard(name);
            }

            this.cardRepository.Add(card);

            return string.Format(OutputMessages.SuccessfullyAddedCard, type, name);
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer targetUser = this.playerRepository.Find(username);
            ICard targetCard = this.cardRepository.Find(cardName);

            targetUser.CardRepository.Add(targetCard);

            return string.Format(OutputMessages.SuccessfullyAddedPlayerWithCards, cardName, username);
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attacker = this.playerRepository.Find(attackUser);
            IPlayer enemy = this.playerRepository.Find(enemyUser);

            IBattleField battleField = new BattleField();

            battleField.Fight(attacker, enemy);

            return string.Format(OutputMessages.FightInfo, attacker.Health, enemy.Health);
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var player in this.playerRepository.Players)
            {
                stringBuilder.AppendLine($"Username: {player.Username} - Health: {player.Health} - Cards {player.CardRepository.Count}");

                if (player.CardRepository.Count > 0)
                {
                    foreach (var card in player.CardRepository.Cards)
                    {
                        stringBuilder.AppendLine($"Card: {card.Name} - Damage: {card.DamagePoints}");
                    }
                }

                stringBuilder.AppendLine("###");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
