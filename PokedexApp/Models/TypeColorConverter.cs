using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls;

namespace PokedexApp.Models
{
    public class TypeColorConverter : IValueConverter
    {
        private Dictionary<PokemonType, Color> _colors = new()
        {
            [PokemonType.Normal] = Color.FromArgb("9fa19f"),
            [PokemonType.Fighting] = Color.FromArgb("ff8000"),
            [PokemonType.Flying] = Color.FromArgb("81b9ef"),     
            [PokemonType.Poison] = Color.FromArgb("9141cb"),
            [PokemonType.Ground] = Color.FromArgb("915121"),
            [PokemonType.Rock] = Color.FromArgb("afa981"),
            [PokemonType.Bug] = Color.FromArgb("91a119"),
            [PokemonType.Ghost] = Color.FromArgb("704170"),
            [PokemonType.Steel] =   Color.FromArgb("60a1b8"),
            [PokemonType.Fire] = Color.FromArgb("e62829"),
            [PokemonType.Water] = Color.FromArgb("2980ef"),
            [PokemonType.Grass] = Color.FromArgb("3fa129"),
            [PokemonType.Electric] = Color.FromArgb("fac000"),
            [PokemonType.Psychic] = Color.FromArgb("ef4179"),
            [PokemonType.Ice] = Color.FromArgb("3fd8ff"),
            [PokemonType.Dragon] = Color.FromArgb("5060e1"),
            [PokemonType.Dark] = Color.FromArgb("50413f"),
            [PokemonType.Fairy] = Color.FromArgb("ef70ef")
        };

        public object? Convert(object? value, System.Type targetType, object? parameter, CultureInfo culture) =>
            (value is PokemonType type && _colors.TryGetValue(type, out var color)) ? color : Colors.HotPink;

        public object? ConvertBack(object? value, System.Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
