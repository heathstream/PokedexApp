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
                >= 0.6 => Colors.SpringGreen,
                >= 0.4 => Colors.Yellow,
                >= 0.2 => Colors.Orange,
                _ => Colors.Red
            };
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
