namespace HotelReservation
{
    public class PriceCalculator
    {
        public decimal GetTotalPrice(decimal pricePerDay, int numberOfDays, Season season, Discount discountType)
        {
            decimal totalPrice = pricePerDay * numberOfDays * (int)season; ;

            totalPrice -= totalPrice * (int)discountType / 100;

            return totalPrice;
        }
    }
}
