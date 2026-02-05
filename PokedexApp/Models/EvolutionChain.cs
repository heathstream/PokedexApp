using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class EvolutionChain
    {
        public ChainLink Chain { get; set; }
        public int Id { get; set; }
    }

    public class ChainLink
    {
        public Pokemon Pokemon { get; set; }
        public int? MinLevel { get; set; }
        public List<ChainLink> EvolvesTo { get; set; }
        public ChainLink EvolvesFrom { get; set; }
    }
}
