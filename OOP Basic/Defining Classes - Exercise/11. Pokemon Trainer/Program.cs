using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var trainers = new List<Trainer>();
        while (true)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if(input[0]== "Tournament")
            {
                break;
            }
            var trainerName = input[0];
            var pokemonName= input[1];
            var pokemonElement=input[2];
            var pokemonHealth = int.Parse(input[3]);

            var pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
            if (trainers.Any(x => x.Name == trainerName) == false)
            {
                var trainer = new Trainer();
                trainer.Name = trainerName;
                trainer.Pokemons.Add(pokemon);
                trainers.Add(trainer);
            }
            else
            {
                var trainer = trainers.SingleOrDefault(x => x.Name == trainerName);
                trainer.Pokemons.Add(pokemon);
            }
        }

        while (true)
        {
            var element = Console.ReadLine();
            if (element == "End")
            {
                break;
            }
            foreach(var e in trainers)
            {
                if (e.Pokemons.Any(x => x.Element == element) == true)
                {
                    e.Badges++;
                }
                else
                {
                    foreach(var p in e.Pokemons)
                    {
                        p.Health -= 10;
                    }
                    e.Pokemons.RemoveAll(x => x.Health <= 0);
                }
            }
        }

        foreach(var t in trainers.OrderByDescending(x => x.Badges))
        {
            Console.WriteLine($"{t.Name} {t.Badges} {t.Pokemons.Count}");
        }
    }
}

