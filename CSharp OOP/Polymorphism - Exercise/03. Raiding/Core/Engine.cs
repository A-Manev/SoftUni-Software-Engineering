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

                if (heroType == "Druid")
                {
                    this.baseHeroes.Add(new Druid(heroName));
                }
                else if (heroType == "Paladin")
                {
                    this.baseHeroes.Add(new Paladin(heroName));
                }
                else if (heroType == "Rogue")
                {
                    this.baseHeroes.Add(new Rogue(heroName));
                }
                else if (heroType == "Warrior")
                {
                    this.baseHeroes.Add(new Warrior(heroName));
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                }
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

            if (bossPower > 0)
            {
                Console.WriteLine("Defeat...");
            }
            else
            {
                Console.WriteLine("Victory!");
            }
        }
    }
}
