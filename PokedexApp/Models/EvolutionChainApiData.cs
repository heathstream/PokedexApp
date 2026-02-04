using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class EvolutionChainApiData
    {
        public chain chain { get; set; }
    }

    public class chain
    {
        public evolution_details evolution_details { get; set; }
        public species species { get; set; }
        public chain? evolves_to { get; set; }
    }

    public class evolution_details
    {
        public int min_level { get; set; }
    }

    public class species
    {
        public string name { get; set; }
    }
}
