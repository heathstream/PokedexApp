using PokedexApp.Models;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        // OnPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Pokemon information
        public Pokemon Pokemon { get; set; }
        public string StrengthsString { get; } = "Strengths:";
        public string WeaknessesString { get; } = "Weaknesses:";
        public string ImmunitiesString { get; } = "Immune to:";
        Dictionary<PokemonType, double> _strengths;
        Dictionary<PokemonType, double> _weaknesses;
        Dictionary<PokemonType, double> _immunities;
        public Dictionary<PokemonType, double> Strengths
        {
            get => _strengths;
            set
            {
                _strengths = value;
                OnPropertyChanged(nameof(Strengths));
            }
        }
        public Dictionary<PokemonType, double> Weaknesses
        {
            get => _weaknesses;
            set
            {
                _weaknesses = value;
                OnPropertyChanged(nameof(Weaknesses));
            }
        }
        public Dictionary<PokemonType, double> Immunities
        {
            get => _immunities;
            set
            {
                _immunities = value;
                OnPropertyChanged(nameof(Immunities));
            }
        }
        public DetailsViewModel(Pokemon pokemon)
        {
            Pokemon = pokemon;
            Strengths = TypeRelations.GetStrengths(pokemon);
            Weaknesses = TypeRelations.GetWeaknesses(pokemon);
            Immunities = TypeRelations.GetImmunities(pokemon);
        }
    }
}
