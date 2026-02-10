using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public static class TypeIcons
    {
        private static Dictionary<PokemonType, string> _iconPaths = new()
        {
            [PokemonType.Normal] = "typeicon_normal.png",
            [PokemonType.Fighting] = "typeicon_fighting.png",
            [PokemonType.Flying] = "typeicon_flying.png",
            [PokemonType.Poison] = "typeicon_poison.png",
            [PokemonType.Ground] = "typeicon_ground.png",
            [PokemonType.Rock] = "typeicon_rock.png",
            [PokemonType.Bug] = "typeicon_bug.png",
            [PokemonType.Ghost] = "typeicon_ghost.png",
            [PokemonType.Steel] = "typeicon_steel.png",
            [PokemonType.Fire] = "typeicon_fire.png",
            [PokemonType.Water] = "typeicon_water.png",
            [PokemonType.Grass] = "typeicon_grass.png",
            [PokemonType.Electric] = "typeicon_electric.png",
            [PokemonType.Psychic] = "typeicon_psychic.png",
            [PokemonType.Ice] = "typeicon_ice.png",
            [PokemonType.Dragon] = "typeicon_dragon.png",
            [PokemonType.Dark] = "typeicon_dark.png",
            [PokemonType.Fairy] = "typeicon_fairy.png"
        };

        public static string GetIcon(PokemonType type) => _iconPaths[type];
    }
}
