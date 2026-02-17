using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class MoveApiData
    {
        public string name { get; set; }
        public int? accuracy { get; set; }
        public int? effect_chance { get; set; }
        public int? pp { get; set; }
        public int? priority { get; set; }
        public int? power { get; set; }
        public type type { get; set; }
        public move_flavor_text[] flavor_text_entries { get; set; }
    }

    public class move_flavor_text
    {
        public string flavor_text { get; set; }
        public language language { get; set; }
    }
}
