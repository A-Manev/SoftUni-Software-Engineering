using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaPresentFactory
{
    public class StartUp
    {
        public static void Main()
        {
            int[] inputMaterials = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            int[] inputMagicLevel = Console.ReadLine()
                 .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            Stack<int> materials = new Stack<int>(inputMaterials);
            Queue<int> magicLevel = new Queue<int>(inputMagicLevel);

            Dictionary<string, int> presents = new Dictionary<string, int>()
            {
                {"Doll", 0 },
                {"Bicycle", 0 },
                {"Teddy bear", 0 },
                {"Wooden train", 0 }
            };

            while (materials.Any() && magicLevel.Any())
            {
                int currentMaterial = materials.Peek();
                int currentMagicLevel = magicLevel.Peek();

                if (currentMaterial == 0 || currentMagicLevel == 0)
                {
                    if (currentMaterial == 0)
                    {
                        materials.Pop();
                    }

                    if (currentMagicLevel == 0)
                    {
                        magicLevel.Dequeue();
                    }

                    continue;
                }

                if (currentMaterial * currentMagicLevel < 0)
                {
                    int sumResult = currentMaterial + currentMagicLevel;
                    materials.Pop();
                    magicLevel.Dequeue();
                    materials.Push(sumResult);
                    continue;
                }

                if (currentMaterial * currentMagicLevel == 150)
                {
                    presents["Doll"]++;
                    materials.Pop();
                    magicLevel.Dequeue();
                }
                else if (currentMaterial * currentMagicLevel == 250)
                {
                    presents["Wooden train"]++;
                    materials.Pop();
                    magicLevel.Dequeue();
                }
                else if (currentMaterial * currentMagicLevel == 300)
                {
                    presents["Teddy bear"]++;
                    materials.Pop();
                    magicLevel.Dequeue();
                }
                else if (currentMaterial * currentMagicLevel == 400)
                {
                    presents["Bicycle"]++;
                    materials.Pop();
                    magicLevel.Dequeue();
                }
                else
                {
                    magicLevel.Dequeue();
                    materials.Push(materials.Pop() + 15);
                }
            }

            bool isSucceeded = false;

            if (presents["Bicycle"] > 0 && presents["Teddy bear"] > 0)
            {
                isSucceeded = true;
            }

            if (presents["Doll"] > 0 && presents["Wooden train"] > 0)
            {
                isSucceeded = true;
            }

            string message = isSucceeded
                ? "The presents are crafted! Merry Christmas!"
                : "No presents this Christmas!";

            Console.WriteLine(message);

            if (materials.Any())
            {
                Console.WriteLine($"Materials left: {string.Join(", ", materials)}");
            }

            if (magicLevel.Any())
            {
                Console.WriteLine($"Magic left: {string.Join(", ", magicLevel)}");
            }

            foreach (var item in presents.OrderBy(x => x.Key))
            {
                if (item.Value > 0)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
        }
    }
}
