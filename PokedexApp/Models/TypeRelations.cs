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

        public static async Task<Dictionary<PokemonType, double>> GetWeaknesses(Pokemon pokemon) => 
            await GetDamageMultipliers(pokemon, dm => dm > 1);

        public static async Task<Dictionary<PokemonType, double>> GetStrengths(Pokemon pokemon) => 
            await GetDamageMultipliers(pokemon, dm => dm < 1 && dm > 0);

        public static async Task<Dictionary<PokemonType, double>> GetImmunities(Pokemon pokemon) =>
            await GetDamageMultipliers(pokemon, dm => dm == 0);

        private static async Task<Dictionary<PokemonType, double>> GetDamageMultipliers(Pokemon pokemon, Predicate<double> predicate)
        {
            Dictionary<PokemonType, double> results = new();
            var typeChart = Chart[pokemon.Types[0]];

            if (pokemon.Types.Count > 1)
                typeChart = await CombineTypeCharts(pokemon);

            foreach (var type in typeChart)
            {
                if (predicate(type.Value))
                    results[type.Key] = type.Value;
            }

            return results.OrderByDescending(t => t.Value).ToDictionary();
        }

        private static async Task<Dictionary<PokemonType, double>> CombineTypeCharts(Pokemon pokemon)
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
