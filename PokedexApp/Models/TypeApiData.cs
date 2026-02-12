using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class TypeApiData
    {
        public string name { get; set; }
        public damage_relations damage_relations { get; set; }
    }

    public class damage_relations
    {
        public List<type> no_damage_to { get; set; }
        public List<type> half_damage_to { get; set; }
        public List<type> double_damage_to { get; set; }
        public List<type> no_damage_from { get; set; }
        public List<type> half_damage_from { get; set; }
        public List<type> double_damage_from { get; set; }
    }

    public class type
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
