using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
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
            if (_colors.TryGetValue(s.ToLower(), out Color? color))
                return color;
            else return Colors.HotPink;
        }
    }
}
