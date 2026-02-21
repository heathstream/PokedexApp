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

        // SKELETON LOADER BOOLS
        [ObservableProperty] bool _isLoading = true;
        [ObservableProperty] bool _isLoaded = false;

        [ObservableProperty] bool _isLoadingMoves = true;
        [ObservableProperty] bool _isLoadedMoves = false;

        // POKEMON PROPERTIES
        [ObservableProperty] Pokemon _pokemon;

        [ObservableProperty] Dictionary<PokemonType, double> _strengths;
        [ObservableProperty] Dictionary<PokemonType, double> _weaknesses;
        [ObservableProperty] Dictionary<PokemonType, double> _immunities;
        [ObservableProperty] Dictionary<PokemonType, double> _damageRelations;

        [ObservableProperty] List<Evolution> _evolvesFrom;
        [ObservableProperty] List<Evolution> _evolvesTo;
        [ObservableProperty] List<Move> _moves;

        public bool HasTwoTypes => (Pokemon.Types.Count == 2);

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
            DamageRelations = Strengths.Concat(Weaknesses).OrderBy(d => d.Value).ToDictionary();

            var evolutionChain = await _service.GetEvolutionChainAsync(Pokemon);
            EvolvesFrom = await evolutionChain.GetEvolvesFrom(Pokemon);
            EvolvesTo = await evolutionChain.GetEvolvesTo(Pokemon);

            await Task.Delay(500);

            IsLoading = false;
            IsLoaded = true;
        }

        public async Task LoadMovesAsync()
        {
            var moveTasks = Pokemon.Moves.Select(async m => await _service.GetMoveAsync(m));
            Moves = (await Task.WhenAll(moveTasks)).ToList();

            IsLoadingMoves = false;
            IsLoadedMoves = true;
        }
    }
}
