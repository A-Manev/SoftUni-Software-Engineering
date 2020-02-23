using System;
using System.Linq;
using System.Collections.Generic;

namespace Lootbox
{
    public class StartUp
    {
        public static void Main()
        {
            int[] inputFirstLootBox = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] inputSecondLootBox = Console.ReadLine()
               .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse)
               .ToArray();

            Queue<int> firstLootBox = new Queue<int>(inputFirstLootBox);
            Stack<int> secondLootBox = new Stack<int>(inputSecondLootBox);

            int loot = 0;

            while (firstLootBox.Any() && secondLootBox.Any())
            {
                int firstBox = firstLootBox.Peek();
                int secondBox = secondLootBox.Peek();
                int sum = firstBox + secondBox;

                if (sum % 2 == 0)
                {
                    loot += sum;
                    firstLootBox.Dequeue();
                    secondLootBox.Pop();
                }
                else
                {
                    firstLootBox.Enqueue(secondLootBox.Pop());
                }
            }

            if (firstLootBox.Count == 0)
            {
                Console.WriteLine("First lootbox is empty");
            }

            if (secondLootBox.Count == 0)
            {
                Console.WriteLine("Second lootbox is empty");
            }

            string isLootEnough = loot >= 100
                ? $"Your loot was epic! Value: {loot}"
                : $"Your loot was poor... Value: {loot}";

            Console.WriteLine(isLootEnough);
        }
    }
}
