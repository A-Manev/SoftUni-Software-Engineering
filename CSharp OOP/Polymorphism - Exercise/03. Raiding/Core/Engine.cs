using System;
using System.Linq;
using System.Collections.Generic;

using Raiding.Models;
using Raiding.Core.Contracts;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private List<BaseHero> baseHeroes;

        public Engine()
        {
            this.baseHeroes = new List<BaseHero>();
        }

        public void Run()
        {
            int numberOfInput = int.Parse(Console.ReadLine());

            while (true)
            {
                if (this.baseHeroes.Count == numberOfInput)
                {
                    break;
                }

                string heroName = Console.ReadLine();

                string heroType = Console.ReadLine();

                string baseNamespace = "Raiding.Models";

                Type type = Type.GetType($"{baseNamespace}.{heroType}");

                if (type == null)
                {
                    Console.WriteLine("Invalid hero!");
                    continue;
                }

                BaseHero hero = (BaseHero)Activator.CreateInstance(type, heroName);

                this.baseHeroes.Add(hero);
            }

            int bossPower = int.Parse(Console.ReadLine());

            if (this.baseHeroes.Any())
            {
                foreach (var hero in this.baseHeroes)
                {
                    bossPower -= hero.Power;

                    Console.WriteLine(hero.CastAbility());
                }
            }

            string finalMessage = bossPower > 0 ? "Defeat..." : "Victory!";

            Console.WriteLine(finalMessage);
        }
    }
}
