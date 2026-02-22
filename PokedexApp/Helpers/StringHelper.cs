using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokedexApp.Helpers
{
    public static class StringHelper
    {
        public static string CleanName(string input)
        {
            var textInfo = new CultureInfo("en-us", true).TextInfo;

            if (input.Any(c => c == '-'))
                input = input.Replace('-', ' ');

            if (input.Any(c => c == '_'))
                input = input.Replace('_', ' ');

            return textInfo.ToTitleCase(input);
        }
    }
}
