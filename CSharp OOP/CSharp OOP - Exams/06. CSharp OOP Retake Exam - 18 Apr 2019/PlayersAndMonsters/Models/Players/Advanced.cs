namespace PlayersAndMonsters.Models.Players
{
    using PlayersAndMonsters.Repositories.Contracts;
    
    public class Advanced : Player
    {
        private const int INITIAL_HEALTH_POINTS = 250;

        public Advanced(ICardRepository cardRepository, string username) 
            : base(cardRepository, username, INITIAL_HEALTH_POINTS)
        {

        }
    }
}
