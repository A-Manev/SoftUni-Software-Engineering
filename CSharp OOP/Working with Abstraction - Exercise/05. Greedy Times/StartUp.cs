using System;
using System.Linq;

namespace GreedyTimes
{
    public class StartUp
    {
        public static void Main()
        {
            long bagCapacity = long.Parse(Console.ReadLine());

            string[] inputTreasure = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Bag bag = new Bag(bagCapacity);

            for (int i = 0; i < inputTreasure.Length; i += 2)
            {
                string treasureName = inputTreasure[i];
                long treasureAmount = long.Parse(inputTreasure[i + 1]);

                InsertItem(bag, treasureName, treasureAmount);
            }

            Console.WriteLine(bag.ToString());
        }

        private static void InsertItem(Bag bag, string treasureName, long treasureAmount)
        {
            if (treasureName.Length >= 4 && treasureName.ToLower() == "gold")
            {
                Gold gold = new Gold(treasureName, treasureAmount);

                bag.AddGoldItem(gold);
            }
            else if (treasureName.Length >= 4 && treasureName.ToLower().EndsWith("gem"))
            {
                Gem gem = new Gem(treasureName, treasureAmount);

                bag.AddGemItem(gem);
            }
            else if (treasureName.Length == 3)
            {
                Cash cash = new Cash(treasureName, treasureAmount);

                bag.AddCashItem(cash);
            }
        }
    }
}