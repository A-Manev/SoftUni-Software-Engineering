using System.Collections.Generic;

namespace PokemonTrainer
{
    public class Trainer
    {
        public Trainer(string name, int badges = 0)
        {
            this.Name = name;
            this.Badges = badges;
            this.CollectionOfPokemons = new List<Pokemon>();
        }

        public string Name { get; set; }

        public int Badges { get; set; }

        public List<Pokemon> CollectionOfPokemons { get; set; }
    }
}
