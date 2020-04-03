namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const int OXYGEN_DECREASE = 5;
        private const int INITIAL_OXYGEN = 70;

        public Biologist(string name) 
            : base(name, INITIAL_OXYGEN)
        {

        }

        public override void Breath()
        {
            if (this.Oxygen - OXYGEN_DECREASE > 0)
            {
                this.Oxygen -= OXYGEN_DECREASE;
            }
            else
            {
                this.Oxygen = 0;
            }
        }
    }
}
