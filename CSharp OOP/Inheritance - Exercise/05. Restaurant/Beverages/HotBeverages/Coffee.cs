namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        private const decimal COFFEE_PRICE = 3.50m;

        private const double COFFEE_MILLILITRES = 50;

        public Coffee(string name, double caffeine) 
            : base(name, COFFEE_PRICE, COFFEE_MILLILITRES)
        {
            this.Caffeine = caffeine;
        }

        public double Caffeine  { get; set; }
    }
}
