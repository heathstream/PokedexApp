using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public static class TypeRelations
    {
        public static Dictionary<PokemonType, Dictionary<PokemonType, double>> Chart = new()
        {
            [PokemonType.Normal] = new() { },
            [PokemonType.Fighting] = new() { },
            [PokemonType.Flying] = new() { },
            [PokemonType.Poison] = new() { },
            [PokemonType.Ground] = new() { },
            [PokemonType.Rock] = new() { },
            [PokemonType.Bug] = new() { },
            [PokemonType.Ghost] = new() { },
            [PokemonType.Steel] = new() { },
            [PokemonType.Fire] = new() { },
            [PokemonType.Water] = new() { },
            [PokemonType.Grass] = new() { },
            [PokemonType.Electric] = new() { },
            [PokemonType.Psychic] = new() { },
            [PokemonType.Ice] = new() { },
            [PokemonType.Dragon] = new() { },
            [PokemonType.Dark] = new() { },
            [PokemonType.Fairy] = new() { }
        };

        public static Dictionary<PokemonType, double> GetWeaknesses(Pokemon pokemon)
        {
            return GetDamageMultipliers(pokemon, dm => dm > 1);
        }

        public static Dictionary<PokemonType, double> GetStrengths(Pokemon pokemon)
        {
            return GetDamageMultipliers(pokemon, dm => dm < 1 && dm > 0);
        }

        public static Dictionary<PokemonType, double> GetImmunities(Pokemon pokemon)
        {
            return GetDamageMultipliers(pokemon, dm => dm == 0);
        }

        public static Dictionary<PokemonType, double> GetDamageMultipliers(Pokemon pokemon, Predicate<double> predicate)
        {
            Dictionary<PokemonType, double> results = new();
            var chart = Chart[pokemon.Types[0]];

            if (pokemon.Types.Count > 1)
                chart = CombineTypeChart(pokemon);

            foreach (var other in chart)
            {
                if (predicate(other.Value))
                    results[other.Key] = other.Value;
            }

            return results.OrderByDescending(t => t.Value).ToDictionary();
        }

        static Dictionary<PokemonType, double> CombineTypeChart(Pokemon pokemon)
        {
            (var t1, var t2) = (pokemon.Types[0], pokemon.Types[1]);

            return Chart[t1].Concat(Chart[t2])
                                .GroupBy(t => t.Key)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(x => x.Value).Aggregate((x, y) => x * y)
                                    );
        }
    }
}
