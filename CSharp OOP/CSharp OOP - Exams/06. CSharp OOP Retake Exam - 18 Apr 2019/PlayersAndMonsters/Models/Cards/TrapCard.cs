namespace PlayersAndMonsters.Models.Cards
{
    public class TrapCard : Card
    {
        private const int DEFAULT_DAMAGE_POINTS = 120;
        private const int DEFAULT_HEALTH_POINTS = 5;

        public TrapCard(string name) 
            : base(name, DEFAULT_DAMAGE_POINTS, DEFAULT_HEALTH_POINTS)
        {

        }
    }
}
