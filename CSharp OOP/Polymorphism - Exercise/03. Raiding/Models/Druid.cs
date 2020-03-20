namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int DRUID_POWER = 80;

        public Druid(string name) 
            : base(name)
        {

        }

        public override int Power => DRUID_POWER;

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
