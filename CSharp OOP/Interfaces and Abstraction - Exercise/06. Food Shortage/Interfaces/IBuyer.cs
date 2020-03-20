namespace FoodShortage.Interfaces
{
    public interface IBuyer : IName
    {
        int Food { get; }

        void BuyFood();
    }
}
