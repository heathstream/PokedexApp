using PokedexApp.Models;

namespace PokedexApp.Views
{
    public partial class DetailsPage : ContentPage
    {
        public List<PokemonType> StrongAgainst;
        public List<PokemonType> WeakAgainst;
        public DetailsPage()
        {
            InitializeComponent();
        }
        public DetailsPage(Pokemon pokemon) : this()
        {
            BindingContext = pokemon;

            if (pokemon.TypeRelations.Count > 1)
            {
                StrongAgainst = pokemon.TypeRelations[0].StrongAgainst
                    .Concat(pokemon.TypeRelations[1].StrongAgainst)
                    .Distinct()
                    .ToList();

                WeakAgainst = pokemon.TypeRelations[0].WeakAgainst
                    .Concat(pokemon.TypeRelations[1].StrongAgainst)
                    .Distinct()
                    .ToList();

                foreach(var t in Enum.GetValues<PokemonType>())
                    if (StrongAgainst.Contains(t) && WeakAgainst.Contains(t))
                    {
                        StrongAgainst.Remove(t);
                        WeakAgainst.Remove(t);
                    }
            }
            else
            {
                StrongAgainst = pokemon.TypeRelations.First().StrongAgainst;
                WeakAgainst = pokemon.TypeRelations.First().WeakAgainst;
            }
            strengthsList.ItemsSource = StrongAgainst;
            weaknessesList.ItemsSource = WeakAgainst;
        }
    }
}
