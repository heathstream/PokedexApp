using PokedexApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Services
{
    internal class PokeApiService
    {
        HttpClient httpClient;

        public PokeApiService()
        {
            httpClient = new HttpClient();
        }
        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
        }
    }
}
