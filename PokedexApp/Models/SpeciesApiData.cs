//using AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class SpeciesApiData
    {
        public flavor_text_entry[] flavor_text_entries { get; set; }
        public NamedApiResource color { get; set; }
        public evolution_chain evolution_chain { get; set; }
    }

    public class flavor_text_entry
    {
        public string flavor_text { get; set; }
        public NamedApiResource language { get; set; }
        public NamedApiResource version { get; set; }
    }

    public class evolution_chain
    {
        public string url { get; set; }
    }
}
