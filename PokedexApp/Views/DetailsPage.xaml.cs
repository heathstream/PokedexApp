using PokedexApp.Models;
using PokedexApp.Services;
using PokedexApp.ViewModels;
using System.ComponentModel;

namespace PokedexApp.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(string pokemonName, PokeApiService service)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel(pokemonName, service);
        }
    }
}
