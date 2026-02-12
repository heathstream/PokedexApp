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
        public List<types> types { get; set; }
        public string flavor_text { get; set; }
        public List<pokemon_stat> stats { get; set; }

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
    //public class type
    //{
    //    public string name { get; set; }
    //    public string url { get; set; }
    //}

    public class types
    {
        public type type { get; set; }
        public int slot { get; set; }
    }

    public class pokemon_stat
    {
        public int effort { get; set; }
        public int base_stat { get; set; }
        public stat stat { get; set; }
    }

    public class stat
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
