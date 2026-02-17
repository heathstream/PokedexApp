using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public partial class Move : ObservableObject
    {
        [ObservableProperty]
        string? _name;

        [ObservableProperty]
        int? _accuracy;

        [ObservableProperty]
        int? _effectChance;

        [ObservableProperty]
        int? _powerPoints;

        [ObservableProperty]
        int? _priority;

        [ObservableProperty]
        int? _power;

        [ObservableProperty]
        PokemonType? _type;

        [ObservableProperty]
        string? _description;

        public Move(MoveApiData data)
        {
            Name = data.name ?? "Unnamed move";
            Accuracy = data.accuracy ?? 0;
            EffectChance = data.effect_chance ?? 0;
            PowerPoints = data.pp ?? 0;
            Priority = data.priority ?? 0;
            Power = data.power ?? 0;
            Type = (data.type != null) ? Enum.Parse<PokemonType>(data.type.name, true) : null;
            Description = (data.flavor_text_entries != null) ? data.flavor_text_entries.Last(t => t.language.name == "en").flavor_text : null;
        }
    }
}
