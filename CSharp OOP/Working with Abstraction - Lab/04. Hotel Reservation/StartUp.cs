using System;
using System.Linq;

namespace HotelReservation
{
    public class StartUp
    {
        public static void Main()
        {
            string[] input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            decimal pricePerDay = decimal.Parse(input[0]);
            int numberOfDays = int.Parse(input[1]);
            Season season = (Season)Enum.Parse(typeof(Season), input[2]);
            Discount discountType = Discount.None;

            if (input.Length == 4)
            {
                discountType = (Discount)Enum.Parse(typeof(Discount), input[3]);
            }

            PriceCalculator priceCalculator = new PriceCalculator();

            decimal totalPrice = priceCalculator.GetTotalPrice(pricePerDay, numberOfDays, season, discountType);

            Console.WriteLine($"{totalPrice:F2}");
        }
    }
}
