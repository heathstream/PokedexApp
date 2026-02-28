using PokedexApp.Services;
using PokedexApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PokedexApp.ViewModels
{
    public class SearchViewModel
    {
        protected CancellationTokenSource _filterCts;
        protected PokeApiService _service = new PokeApiService();

        public List<ListItem> AllResults = new();
        public ObservableCollection<ListItem> FilteredResults { get; set; } = new();

        public SearchViewModel(PokeApiService service)
        {
            _service = service;
        }

        public virtual async Task LoadAllResults()
        {
            if (!AllResults.Any()) return;
            AllResults.AddRange(await _service.GetPokemonListItemsAsync());
        }

        public async void FilterResults(string? searchText)
        {
            // To reduce lag, I create a cancellation token which gets cancelled at the start of the method. This way, only when the user
            // has stopped typing for 250ms will the method continue. Otherwise, when a new letter is typed the method for the old letter
            // will throw an exception and be stopped.

            _filterCts?.Cancel();
            _filterCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(250, _filterCts.Token);
                var text = searchText?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(text))
                {
                    FilteredResults.Clear();
                    return;
                }

                var filteredPokemon = AllResults.Where(p => p.Name.ToLower().Contains(text.ToLower()));
                FilteredResults.Clear();
                foreach (var p in filteredPokemon)
                    FilteredResults.Add(p);
            }
            catch (TaskCanceledException) { }
        }
    }
}
