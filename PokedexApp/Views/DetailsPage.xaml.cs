using PokedexApp.Models;
using System.ComponentModel;

namespace PokedexApp.Views
{
    public partial class DetailsPage : ContentPage, INotifyPropertyChanged
    {
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
        public DetailsPage()
        {
            InitializeComponent();
        }
        public DetailsPage(Pokemon pokemon) : this()
        {
            BindingContext = pokemon;

            Strengths = TypeRelations.GetStrengths(pokemon);
            Weaknesses = TypeRelations.GetWeaknesses(pokemon);
            Immunities = TypeRelations.GetImmunities(pokemon);

            //strengthsList.BindingContext = Strengths;
            //weaknessesList.ItemsSource = Weaknesses;
            //immunitiesList.ItemsSource = Immunities;
        }
    }
}
