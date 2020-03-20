namespace Raiding.Models
{
    public abstract class BaseHero
    {
        protected BaseHero(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public virtual int Power { get;  private set; }

        public abstract string CastAbility();
    }
}
