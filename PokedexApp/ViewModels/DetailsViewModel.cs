using PokedexApp.Models;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using PokedexApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PokedexApp.ViewModels
{
    public partial class DetailsViewModel : ObservableObject, INotifyPropertyChanged
    {
        PokeApiService _service;

        // POKEMON PROPERTIES
        [ObservableProperty]
        Pokemon _pokemon;

        [ObservableProperty]
        Dictionary<PokemonType, double> _strengths;

        [ObservableProperty]
        Dictionary<PokemonType, double> _weaknesses;

        [ObservableProperty]
        Dictionary<PokemonType, double> _immunities;

        public DetailsViewModel(string pokemonName, PokeApiService service)
        {
            _service = service;
            Task.Run(() => LoadAsync(pokemonName));
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
