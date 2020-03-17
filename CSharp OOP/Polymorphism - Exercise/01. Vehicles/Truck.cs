namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double ADDITIONAL_CONSUMPTION_PER_KM = 1.6;
        private const double REFUEL_EFFICIENCY_PERCENTAGE = 0.95;

        public Truck(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption)
        {

        }

        protected override double AdditionalConsumption => ADDITIONAL_CONSUMPTION_PER_KM;

        public override void Refuel(double fuel)
        {
            base.Refuel(fuel * REFUEL_EFFICIENCY_PERCENTAGE);
        }
    }
}
