//using AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class SpeciesApiData
    {
        public flavor_text_entry[] flavor_text_entries { get; set; }
        public color color { get; set; }
        public evolution_chain evolution_chain { get; set; }
    }

    public class flavor_text_entry
    {
        public string flavor_text { get; set; }
        public language language { get; set; }
        public version version { get; set; }
    }

    public class language
    {
        public string name { get; set; }
    }

    public class color
    {
        public string name { get; set; }
    }

    public class evolution_chain
    {
        public string url { get; set; }
    }

    public class version
    {
        public string name { get; set; }
    }
}
