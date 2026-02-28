using PokedexApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.ViewModels
{
    public class ItemSearchViewModel : SearchViewModel
    {
        public ItemSearchViewModel(PokeApiService service) : base(service)
        {

        }

        public override async Task LoadAllResults()
        {
            if (!AllResults.Any()) return;
            AllResults.AddRange(await _service.GetItemListItemsAsync());
        }
    }
}
