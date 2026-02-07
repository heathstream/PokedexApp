using PokedexApp.Models;
using PokedexApp.ViewModels;
using System.ComponentModel;

namespace PokedexApp.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Pokemon pokemon)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel(pokemon);
        }
    }
}
