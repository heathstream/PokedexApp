using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PokedexApp.Models
{
    public class Pokemon
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<PokemonType> Types { get; init; }
        public List<PokemonTypeRelations> TypeRelations { get; set; }
        public double Weight { get; init; }
        public double Height { get; init; }
        public string Sprite { get; init; }
        public string Description { get; init; }
        public List<Pokemon>? EvolutionChain { get; set; }
        public Color Color { get; set; }

        public Pokemon(PokemonApiData pData, SpeciesApiData sData)
        {
            Id = pData.id;
            Name = pData.name.Substring(0,1).ToUpper() + pData.name.Substring(1);
            Weight = pData.weight / 10;
            Height = pData.height / 10;
            Sprite = pData.sprites.front_default;
            Types = pData.types.Select(t => Enum.Parse<PokemonType>(t.type.name, true)).ToList();

            Color = PokemonColors.FromString(sData.color.name);
            Description = sData.flavor_text_entries
                .First(t => t.language.name == "en")
                .flavor_text
                .Replace('\n', ' ')
                .Replace('\f', ' ');

            TypeRelations = new();
        }
    }
    public enum PokemonType
    {
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
        Fairy
    }

    public class PokemonTypeRelations
    {
        public PokemonType Type { get; init; }
        public List<PokemonType> StrongAgainst { get; init; } = new();
        public List<PokemonType> WeakAgainst { get; init; } = new();

        public PokemonTypeRelations(TypeApiData data)
        {
            Type = Enum.Parse<PokemonType>(data.name, true);

            StrongAgainst = data.damage_relations.double_damage_to
                //.Concat(data.damage_relations.half_damage_from)
                //.Concat(data.damage_relations.no_damage_from)
                //.Distinct()
                .Select(t => Enum.Parse<PokemonType>(t.name, true))
                .ToList();

            WeakAgainst = data.damage_relations.double_damage_from
                //.Concat(data.damage_relations.half_damage_to)
                //.Concat(data.damage_relations.no_damage_to)
                //.Distinct()
                .Select(t => Enum.Parse<PokemonType>(t.name, true))
                .ToList();
        }
    }

        //public static class PokemonTypeChart
        //{
        //    private static Dictionary<PokemonType, Dictionary<PokemonType, double>> _chart = new()
        //    {
        //        [PokemonType.Fire] = new()
        //        {
        //            [PokemonType.Fire] = 0.5f,
        //            [PokemonType.Water] = 0.5f,
        //            [PokemonType.Grass] = 2.0f,
        //            [PokemonType.Ice] = 2.0f,
        //            [PokemonType.Bug] = 2.0f,
        //            [PokemonType.Rock] = 2.0f,
        //            [PokemonType.Grass] = 2.0f,
        //            [PokemonType.Grass] = 2.0f,
        //        }
        //    };
        //}

        public static class PokemonColors
    {
        private static Dictionary<string, Color> _colors = new()
        {
            { "black", Color.FromArgb("010101") },
            { "blue", Color.FromArgb("6284b1") },
            { "brown", Color.FromArgb("aa7d5f") },
            { "gray", Color.FromArgb("a1a0a7") },
            { "green", Color.FromArgb("8cc547") },
            { "pink", Color.FromArgb("fbaab5") },
            { "purple", Color.FromArgb("a17aa5") },
            { "red", Color.FromArgb("ce5117") },
            { "white", Color.FromArgb("d1dde7") },
            { "yellow", Color.FromArgb("f9d001") },
        };

        public static Color FromString(string s)
        {
            if (_colors.TryGetValue(s, out Color? color))
                return color;
            else return Colors.HotPink;
        }
    }
}
