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
        public List<Type> no_damage_to { get; set; }
        public List<Type> half_damage_to { get; set; }
        public List<Type> double_damage_to { get; set; }
        public List<Type> no_damage_from { get; set; }
        public List<Type> half_damage_from { get; set; }
        public List<Type> double_damage_from { get; set; }
    }

    public class type
    {
        public string name { get; set; }
    }
}
