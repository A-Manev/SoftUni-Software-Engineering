using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyRevolver
{
    class StartUp
    {
        static void Main()
        {
            int bulletPrice = int.Parse(Console.ReadLine());
            int gunBarrelSize = int.Parse(Console.ReadLine());
            int[] InputBullets = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int[] InputLooks = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int intelligenceValue = int.Parse(Console.ReadLine());

            Stack<int> bullets = new Stack<int>(InputBullets);
            Queue<int> looks = new Queue<int>(InputLooks);

            int barrelBullets = gunBarrelSize;
            while (bullets.Any() && looks.Any())
            {
                int bullet = bullets.Pop();
                int cuttrntLook = looks.Peek();

                if (bullet <= cuttrntLook)
                {
                    Console.WriteLine("Bang!");
                    looks.Dequeue();
                }
                else
                {
                    Console.WriteLine("Ping!");
                }

                intelligenceValue -= bulletPrice;
                barrelBullets--;

                if (barrelBullets == 0 && bullets.Any())
                {
                    Console.WriteLine("Reloading!");
                    barrelBullets = gunBarrelSize;
                }
            }

            if (!bullets.Any() && looks.Any())
            {
                Console.WriteLine($"Couldn't get through. Locks left: {looks.Count}");
            }
            else
            {
                Console.WriteLine($"{bullets.Count} bullets left. Earned ${intelligenceValue}");
            }
        }
    }
}
