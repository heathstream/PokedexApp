using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using PokedexApp.Helpers;

namespace PokedexApp.Models
{
    public partial class Move : ObservableObject
    {
        [ObservableProperty]
        string _name;

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
        DamageClass? _dmgClass;

        [ObservableProperty]
        string? _description;

        public Move(MoveApiData data)
        {
            Name = StringHelper.CleanName(data.name) ?? "Unnamed Move";
            Accuracy = data.accuracy ?? null;
            EffectChance = data.effect_chance ?? null;
            PowerPoints = data.pp ?? null;
            Priority = data.priority ?? null;
            Power = data.power ?? null;
            Type = (data.type != null) ? Enum.Parse<PokemonType>(data.type.name, true) : null;
            DmgClass = (data.type != null) ? Enum.Parse<DamageClass>(data.damage_class.name, true) : null;
            Description = (data.flavor_text_entries != null) ? data.flavor_text_entries.Last(t => t.language.name == "en").flavor_text : null;
        }
    }

    public enum DamageClass
    {
        Status,
        Physical,
        Special
    }
}
