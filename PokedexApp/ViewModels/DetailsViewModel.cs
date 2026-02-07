using PokedexApp.Models;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Pokemon Pokemon;
        public string StrengthsString { get; } = "Strengths:";
        public string WeaknessesString { get; } = "Weaknesses:";
        public string ImmunitiesString { get; } = "Immune to:";
        Dictionary<PokemonType, double> _strengths;
        Dictionary<PokemonType, double> _weaknesses;
        Dictionary<PokemonType, double> _immunities;
        public Dictionary<PokemonType, double> Strengths
        {
            get { return _strengths; }
            set
            {
                _strengths = value;
                OnPropertyChanged(nameof(Strengths));
            }
        }
        public Dictionary<PokemonType, double> Weaknesses
        {
            get { return _weaknesses; }
            set
            {
                _weaknesses = value;
                OnPropertyChanged(nameof(Weaknesses));
            }
        }
        public Dictionary<PokemonType, double> Immunities
        {
            get { return _immunities; }
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
