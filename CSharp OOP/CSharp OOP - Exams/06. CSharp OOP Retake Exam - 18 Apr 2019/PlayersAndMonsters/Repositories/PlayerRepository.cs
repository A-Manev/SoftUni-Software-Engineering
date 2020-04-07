namespace PlayersAndMonsters.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using PlayersAndMonsters.Utilities.Messages;
    using PlayersAndMonsters.Repositories.Contracts;
    using PlayersAndMonsters.Models.Players.Contracts;

    public class PlayerRepository : IPlayerRepository
    {
        private List<IPlayer> players;

        public PlayerRepository()
        {
            this.players = new List<IPlayer>();
        }

        public int Count => this.Players.Count;

        public IReadOnlyCollection<IPlayer> Players => this.players.AsReadOnly();

        public void Add(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException(ExceptionMessages.NullPlayer);
            }

            if (this.players.Any(p => p.Username == player.Username))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPlayer, player.Username));
            }

            this.players.Add(player);
        }

        public bool Remove(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException(ExceptionMessages.NullPlayer);
            }

            if (!this.players.Any(p => p.Username == player.Username)) // CARE HERE
            {
                return false;
            }

            this.players.Remove(player);

            return true;
        }

        public IPlayer Find(string username)
        {
            return this.players.FirstOrDefault(p => p.Username == username);
        }
    }
}
