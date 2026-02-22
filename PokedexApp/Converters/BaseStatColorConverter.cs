using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokedexApp.Converters
{
    public class BaseStatColorConverter : IValueConverter
    {
        public Color MaxColor = Color.FromArgb("00ff00");
        public Color MinColor = Color.FromArgb("ff0000");
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not int stat)
                return Colors.HotPink;

            double normalized = stat / 200.0;

            return normalized switch
            {
                >= 0.8 => Color.FromArgb("3fd8ff"),
                >= 0.6 => Color.FromArgb("3fa129"),
                >= 0.4 => Color.FromArgb("fac000"),
                >= 0.2 => Color.FromArgb("ff8000"),
                _ => Color.FromArgb("e62829")
            };
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
