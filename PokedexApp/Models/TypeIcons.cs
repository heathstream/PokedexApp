using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public static class TypeIcons
    {
        private static Dictionary<PokemonType, string> _iconPaths = new()
        {
            [PokemonType.Normal] = "",
            [PokemonType.Fighting] = "",
            [PokemonType.Flying] = "",
            [PokemonType.Poison] = "",
            [PokemonType.Ground] = "",
            [PokemonType.Rock] = "",
            [PokemonType.Bug] = "",
            [PokemonType.Ghost] = "",
            [PokemonType.Steel] = "",
            [PokemonType.Fire] = "" ,
            [PokemonType.Water] = "",
            [PokemonType.Grass] = "",
            [PokemonType.Electric] = "",
            [PokemonType.Psychic] = "",
            [PokemonType.Ice] = "",
            [PokemonType.Dragon] = "",
            [PokemonType.Dark] = "",
            [PokemonType.Fairy] = ""
        };

        public static string GetIcon(PokemonType type) => _iconPaths[type];
    }
}
