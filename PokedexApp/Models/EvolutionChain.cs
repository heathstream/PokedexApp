using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class EvolutionChain
    {
        public ChainLink Chain { get; set; }
        public int Id { get; set; }

        public async Task<List<Pokemon>> GetEvolvesFrom(Pokemon pokemon)
        {
            List<Pokemon> evolvesFrom = new();
            ProcessLink(Chain);
            return evolvesFrom;

            async void ProcessLink(ChainLink cl)
            {
                if (cl.Pokemon == pokemon) return;
                evolvesFrom.Add(cl.Pokemon);

                if (cl.EvolvesTo != null && cl.EvolvesTo.Any())
                {
                    if (cl.EvolvesTo.Any(cl => cl.Pokemon == pokemon))
                        return;
                    ProcessLink(cl.EvolvesTo.First());
                }
                
            }
        }
        public async Task<List<Pokemon>> GetEvolvesTo(Pokemon pokemon)
        {
            List<Pokemon> evolvesTo = new();
            ProcessChain(Chain);
            return evolvesTo;

            void ProcessChain(ChainLink cl)
            {
                if (cl.EvolvesTo != null)
                {
                    if (cl.Pokemon == pokemon)
                    {
                        if (cl.EvolvesTo != null)
                            foreach (var link in cl.EvolvesTo)
                                evolvesTo.Add(link.Pokemon);
                        return;
                    }
                    if (cl.EvolvesTo.Any())
                        ProcessChain(cl.EvolvesTo.First());
                }
            }
        }
    }

    public class ChainLink
    {
        public Pokemon Pokemon { get; set; }
        public int? MinLevel { get; set; }
        public List<ChainLink> EvolvesTo { get; set; }

        //public ChainLink? EvolvesFrom { get; set; }
    }
}
