using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    class StartUp
    {
        static void Main()
        {
            List<Trainer> trainers = new List<Trainer>();

            string command = Console.ReadLine();

            while (command != "Tournament")
            {
                string[] commandArguments = command
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string trainerName = commandArguments[0];
                string pokemonName = commandArguments[1];
                string pokemonElement = commandArguments[2];
                int pokemonHealth = int.Parse(commandArguments[3]);

                Trainer trainer = new Trainer(trainerName);
                Pokemon pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);

                var targetTrainer = trainers.FirstOrDefault(x => x.Name == trainerName);

                if (targetTrainer == null)
                {
                    trainers.Add(trainer);
                    trainer.CollectionOfPokemons.Add(pokemon);
                }
                else
                {
                    targetTrainer.CollectionOfPokemons.Add(pokemon);
                }

                command = Console.ReadLine();
            }

            string InputElement = Console.ReadLine();

            while (InputElement != "End")
            {
                foreach (var currentTrainer in trainers)
                {
                    if (currentTrainer.CollectionOfPokemons.Any(x => x.Element == InputElement))
                    {
                        currentTrainer.Badges++;
                    }
                    else
                    {
                        if (currentTrainer.CollectionOfPokemons.Any())
                        {
                            foreach (var pokemon in currentTrainer.CollectionOfPokemons.OrderByDescending(x=>x.Health))
                            {
                                pokemon.Health -= 10;

                                if (pokemon.Health <= 0)
                                {
                                    currentTrainer.CollectionOfPokemons.Remove(pokemon);

                                    if (!currentTrainer.CollectionOfPokemons.Any())
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                InputElement = Console.ReadLine();
            }

            foreach (var trainer in trainers.OrderByDescending(x => x.Badges))
            {
                Console.WriteLine($"{trainer.Name} {trainer.Badges} {trainer.CollectionOfPokemons.Count}");
            }
        }
    }
}
