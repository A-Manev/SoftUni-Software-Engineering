namespace PlayersAndMonsters.Models.Cards
{
    public class MagicCard : Card
    {
        private const int DEFAULT_DAMAGE_POINTS = 5;
        private const int DEFAULT_HEALTH_POINTS = 80;

        public MagicCard(string name) 
            : base(name, DEFAULT_DAMAGE_POINTS, DEFAULT_HEALTH_POINTS)
        {

        }
    }
}
