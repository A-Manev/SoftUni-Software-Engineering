namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        private const int INITIAL_OXYGEN = 50;

        public Geodesist(string name) 
            : base(name, INITIAL_OXYGEN)
        {

        }
    }
}
