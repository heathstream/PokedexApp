using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public enum PokemonStat
    {
        HP,
        Attack,
        Defense,
        SpecialAttack,
        SpecialDefense,
        Speed
    }

    public class BaseStat
    {
        public PokemonStat Stat { get; set; }
        public string StatString => _strings[Stat];
        public int Number { get; set; }
        public double NumberNormalized => Number / 200.0;
        public int Effort { get; set; }

        private Dictionary<PokemonStat, string> _strings = new()
        {
            [PokemonStat.HP] = "HP",
            [PokemonStat.Attack] = "Attack",
            [PokemonStat.Defense] = "Defense",
            [PokemonStat.SpecialAttack] = "Sp. Atk",
            [PokemonStat.SpecialDefense] = "Sp. Def",
            [PokemonStat.Speed] = "Speed"
        };
    }
}
