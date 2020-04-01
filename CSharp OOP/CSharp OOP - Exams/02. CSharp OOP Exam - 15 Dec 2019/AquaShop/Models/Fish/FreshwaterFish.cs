namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int INITIAL_SIZE = 3;

        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {

        }

        public int Size => INITIAL_SIZE;

        public override void Eat()
        {
            base.Size += 3;
        }
    }
}
