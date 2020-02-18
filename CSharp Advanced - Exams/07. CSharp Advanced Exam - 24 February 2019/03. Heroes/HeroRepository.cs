using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Heroes
{
    public class HeroRepository
    {
        private List<Hero> data;

        public HeroRepository()
        {
            this.data = new List<Hero>();
        }

        public int Count => this.data.Count;

        public void Add(Hero hero)
        {
            this.data.Add(hero);
        }

        public void Remove(string name)
        {
            var target = data.FirstOrDefault(x => x.Name == name);

            data.Remove(target);
        }

        public Hero GetHeroWithHighestStrength()
        {
            Hero heroWithHighestStrength = null;

            int maxStrength = int.MinValue;

            foreach (var hero in this.data)
            {
                if (hero.Item.Strength > maxStrength)
                {
                    maxStrength = hero.Item.Strength;
                    heroWithHighestStrength = hero;
                }
            }

            return heroWithHighestStrength;
        }

        public Hero GetHeroWithHighestAbility()
        {
            Hero heroWithHighestAbility = null;

            int maxAbility = int.MinValue;

            foreach (var hero in this.data)
            {
                if (hero.Item.Ability > maxAbility)
                {
                    maxAbility = hero.Item.Ability;
                    heroWithHighestAbility = hero;
                }
            }

            return heroWithHighestAbility;
        }

        public Hero GetHeroWithHighestIntelligence()
        {
            Hero heroWithHighestIntelligence = null;

            int maxIntelligence = int.MinValue;

            foreach (var hero in this.data)
            {
                if (hero.Item.Intelligence > maxIntelligence)
                {
                    maxIntelligence = hero.Item.Intelligence;
                    heroWithHighestIntelligence = hero;
                }
            }

            return heroWithHighestIntelligence;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in this.data)
            {
                stringBuilder.AppendLine(item.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
