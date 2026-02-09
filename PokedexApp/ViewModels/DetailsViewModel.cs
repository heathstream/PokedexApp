using PokedexApp.Models;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using PokedexApp.Services;

namespace PokedexApp.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        PokeApiService _service;

        // OnPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Pokemon information
        Pokemon _pokemon;
        public Pokemon Pokemon
        {
            get => _pokemon;
            set
            {
                _pokemon = value;
                OnPropertyChanged(nameof(Pokemon));
            }
        }
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
        public DetailsViewModel(string pokemonName, PokeApiService service)
        {
            _service = service;
            LoadAsync(pokemonName);
        }

        public async Task LoadAsync(string pokemonName)
        {
            Pokemon = await _service.GetPokemonAsync(pokemonName);
            Strengths = await TypeRelations.GetStrengths(Pokemon);
            Weaknesses = await TypeRelations.GetWeaknesses(Pokemon);
            Immunities = await TypeRelations.GetImmunities(Pokemon);
        }
    }
}
