namespace PlayersAndMonsters.Models.Players
{
    using PlayersAndMonsters.Repositories.Contracts;

    public class Beginner : Player
    {
        private const int INITIAL_HEALTH_POINTS = 50;

        public Beginner(ICardRepository cardRepository, string username)
            : base(cardRepository, username, INITIAL_HEALTH_POINTS)
        {

        }
    }
}
