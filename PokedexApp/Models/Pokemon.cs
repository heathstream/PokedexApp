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
        public List<Pokemon>? EvolutionChain { get; set; }
        public Color Color { get; set; }

        public Pokemon(PokemonApiData pData, SpeciesApiData sData)
        {
            Id = pData.id;
            Name = pData.name.Substring(0, 1).ToUpper() + pData.name.Substring(1);
            Weight = pData.weight / 10;
            Height = pData.height / 10;
            Sprite = pData.sprites.other.official_artwork.front_default;
            Types = pData.types.Select(t => Enum.Parse<PokemonType>(t.type.name, true)).ToList();

            Color = PokemonColors.FromString(sData.color.name);
            Description = sData.flavor_text_entries
                .Last(t => t.language.name == "en")
                .flavor_text
                .Replace('\n', ' ')
                .Replace('\f', ' ');
        }

        public bool Equals(Pokemon? other)
        {
            return (Name == other?.Name);
        }
    }
}
