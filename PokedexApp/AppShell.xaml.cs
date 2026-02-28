using PokedexApp.Views;

namespace PokedexApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetNavBarIsVisible(this, false);

            //Routing.RegisterRoute("PokemonSearchPage", typeof(PokemonSearchPage));
            //Routing.RegisterRoute("PokemonSearchPage", typeof(SearchPage));
            //Routing.RegisterRoute("ItemSearchPage", typeof(SearchPage));
            //Routing.RegisterRoute("MoveSearchPage", typeof(SearchPage));
        }
    }
}
