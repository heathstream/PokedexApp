using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public partial class Move : ObservableObject
    {
        [ObservableProperty]
        string _name;

        [ObservableProperty]
        int _accuracy;

        [ObservableProperty]
        int _effectChance;

        [ObservableProperty]
        int _powerPoints;

        [ObservableProperty]
        int _priority;

        [ObservableProperty]
        int _power;

        [ObservableProperty]
        PokemonType _type;

        public Move(MoveApiData data)
        {
            Name = data.name;
            Accuracy = data.accuracy;
            EffectChance = data.effect_chance;
            PowerPoints = data.pp;
            Priority = data.priority;
            Power = data.power;
            Type = Enum.Parse<PokemonType>(data.type.name);
        }
    }
}
