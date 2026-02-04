//using AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokedexApp.Models
{
    public class PokemonApiData
    {
        public int id { get; set; }
        public string name { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public Sprites sprites { get; set; }
        public List<Types> types { get; set; }
        public string flavor_text { get; set; }

    }

    // SPRITE
    public class Sprites
    {
        //public string front_default { get; set; }
        public other other { get; set; }
    }
    public class other
    {
        [JsonPropertyName("official-artwork")]
        public official_artwork official_artwork { get; set; }
    }
    public class official_artwork
    {
        public string front_default { get; set; }
    }

    // TYPE
    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Types
    {
        public Type type { get; set; }
        public int slot { get; set; }
    }
}
