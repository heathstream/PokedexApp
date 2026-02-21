using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class EvolutionChainApiData
    {
        public int id { get; set; }
        public chainlink chain { get; set; }
    }

    public class chainlink
    {
        public List<evolution_detail> evolution_details { get; set; }
        public species species { get; set; }
        public List<chainlink> evolves_to { get; set; }
    }

    public class evolution_detail
    {
        public int? min_level { get; set; }
        public NamedApiResource? item { get; set; }
    }

    public class species
    {
        public string name { get; set; }
    }
}
