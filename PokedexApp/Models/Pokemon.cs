using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PokedexApp.Models
{
    public class Pokemon : IEquatable<Pokemon>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<PokemonType> Types { get; init; }
        public double Weight { get; init; }
        public double Height { get; init; }
        public string Sprite { get; init; }
        public string Description { get; init; }
        public Color Color { get; set; }
        public string EvolutionChainUrl { get; set; }
        public List<BaseStat> BaseStats { get; set; } = new();

        public Pokemon(PokemonApiData pData, SpeciesApiData sData)
        {
            Id = pData.id;
            Name = pData.name.Substring(0, 1).ToUpper() + pData.name.Substring(1);
            Weight = pData.weight / 10;
            Height = pData.height / 10;
            Sprite = pData.sprites.other.official_artwork.front_default;
            Types = pData.types.Select(t => Enum.Parse<PokemonType>(t.type.name, true)).ToList();
            EvolutionChainUrl = sData.evolution_chain.url;
            Color = PokemonColors.FromString(sData.color.name);
            Description = sData.flavor_text_entries
                .Last(t => t.language.name == "en" && t.version.name != "legends-arceus")
                .flavor_text
                .Replace('\n', ' ')
                .Replace('\f', ' ');
            BaseStats = pData.stats.Select(s => new BaseStat()
            {
                Number = s.base_stat,
                Effort = s.effort,
                Stat = Enum.Parse<PokemonStat>(s.stat.name.Replace("-", ""), true)
            }).ToList();
        }

        public bool Equals(Pokemon? other)
        {
            return (Name == other?.Name);
        }
    }
}
