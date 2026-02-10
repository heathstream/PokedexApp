using PokedexApp.Models;
using PokedexApp.Services;
using PokedexApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PokedexApp
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource _filterCts;
        PokeApiService _service = new PokeApiService();
        List<PokemonListItem> _fullPokemonList = new();
        public ObservableCollection<PokemonListItem> FilteredPokemonList { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (_fullPokemonList.Any()) return;
            Task.Run(() => LoadPokemonListItems());
        }

        public async Task LoadPokemonListItems()
        {
            _fullPokemonList = await _service.GetPokemonListItemsAsync();
            foreach (var p in _fullPokemonList)
                FilteredPokemonList.Add(p);
        }

        //public async Task LoadAllPokemon()
        //{
        //    _fullPokemonList = await _service.GetAllPokemonAsync();
        //    foreach (var p in _fullPokemonList)
        //        FilteredPokemonList.Add(p);
        //    pokemonList.ItemsSource = FilteredPokemonList;
        //}

        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // To reduce lag, I create a cancellation token which gets cancelled at the start of the method. This way, only when the user
            // has stopped typing for 250ms will the method continue. Otherwise, when a new letter is typed the method for the old letter
            // will throw an exception and be stopped.

            _filterCts?.Cancel();
            _filterCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(250, _filterCts.Token);
                var text = e.NewTextValue?.Trim() ?? string.Empty;
                
                if (string.IsNullOrWhiteSpace(text))
                {
                    FilteredPokemonList.Clear();
                    return;
                }

                var filteredPokemon = _fullPokemonList.Where(p => p.Name.Contains(text));
                FilteredPokemonList.Clear();
                foreach (var p in filteredPokemon)
                    FilteredPokemonList.Add(p);
            }
            catch (TaskCanceledException) { }
        }

        private async void pokemonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is PokemonListItem item)
            {
                try
                {
                    //var pokemon = await _service.GetPokemonAsync(item.Name);
                    //await Navigation.PushAsync(new DetailsPage(pokemon));
                    await Navigation.PushAsync(new DetailsPage(item.Name, _service));
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("Error", $"Could not load that Pokémon! :(\n{ex.Message}", "OK :(");
                }
                finally
                {
                    pokemonList.SelectedItem = null;
                }
            }
        }
    }
}
