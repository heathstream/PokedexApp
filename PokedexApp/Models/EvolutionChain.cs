using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class EvolutionChain
    {
        public Evolution Chain { get; set; }
        public int Id { get; set; }

        public async Task<List<Evolution>> GetEvolvesFrom(Pokemon pokemon)
        {
            List<Evolution> evolvesFrom = new();
            ProcessLink(Chain);
            return evolvesFrom;

            async void ProcessLink(Evolution e)
            {
                if (e.Pokemon == pokemon) return;
                evolvesFrom.Add(e);

                if (e.EvolvesTo != null && e.EvolvesTo.Any())
                {
                    if (e.EvolvesTo.Any(cl => cl.Pokemon == pokemon))
                        return;
                    ProcessLink(e.EvolvesTo.First());
                }
                
            }
        }
        public async Task<List<Evolution>> GetEvolvesTo(Pokemon pokemon)
        {
            List<Evolution> evolvesTo = new();
            ProcessChain(Chain);
            return evolvesTo;

            void ProcessChain(Evolution e)
            {
                if (e.EvolvesTo != null)
                {
                    if (e.Pokemon == pokemon)
                    {
                        if (e.EvolvesTo != null)
                            foreach (var evolution in e.EvolvesTo)
                                evolvesTo.Add(evolution);
                        return;
                    }
                    if (e.EvolvesTo.Any())
                        ProcessChain(e.EvolvesTo.First());
                }
            }
        }
    }

    public class Evolution
    {
        public Pokemon Pokemon { get; set; }
        public EvolutionDetails Details { get; set; }
        public List<Evolution> EvolvesTo { get; set; }
    }

    public class EvolutionDetails
    {
        public EvolutionTrigger Trigger { get; set; }
        public Item? Item { get; set; }
        public int? MinLevel { get; set; }
    }

    public enum EvolutionTrigger
    {
        LevelUp,
        Trade,
        UseItem,
        Shed,
        Spin,
        TowerOfDarkness,
        TowerOfWaters,
        ThreeCriticalHits,
        TakeDamage,
        Other,
        AgileStyleMove,
        StrongStyleMove,
        RecoilDamage,
        UseMove,
        ThreeDefeatedBisharp,
        GimmighoulCoins
    }
}
