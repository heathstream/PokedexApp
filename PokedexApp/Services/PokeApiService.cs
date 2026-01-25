using PokedexApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;

namespace PokedexApp.Services
{
    internal class PokeApiService
    {
        const int NO_OF_POKEMON = 151;

        HttpClient httpClient;

        Dictionary<string, Pokemon> _pokemonCache = new();
        Dictionary<PokemonType, PokemonTypeRelations> _typeRelationsCache = new();
        List<PokemonListItem> _pkmnListItemCache = new();

        public PokeApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (_pokemonCache.TryGetValue(name, out Pokemon? cachedPokemon))
                return cachedPokemon;

            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
            PokemonApiData? pokemonApiData = await httpClient.GetFromJsonAsync<PokemonApiData>(uri);
            uri = $"https://pokeapi.co/api/v2/pokemon-species/{name}";
            SpeciesApiData? speciesApiData = await httpClient.GetFromJsonAsync<SpeciesApiData>(uri);

            var pokemon = new Pokemon(pokemonApiData, speciesApiData);

            foreach (var type in pokemon.Types)
                pokemon.TypeRelations.Add(await GetTypeRelationsAsync(type));

            _pokemonCache[name] = pokemon;
            return pokemon;
        }

        public async Task<List<PokemonListItem>> GetPokemonListItemsAsync()
        {
            if (_pkmnListItemCache.Any())
                return _pkmnListItemCache;

            var uri = $"https://pokeapi.co/api/v2/pokemon?limit={NO_OF_POKEMON}";

            PokemonListResponse? response = await httpClient.GetFromJsonAsync<PokemonListResponse>(uri);

            var listItems = response.Results;
            _pkmnListItemCache.AddRange(listItems);
            return listItems;
        }

        public async Task<PokemonTypeRelations> GetTypeRelationsAsync(PokemonType type)
        {
            //if (_typeRelationsCache.TryGetValue(type, out PokemonTypeRelations? ptr))
            //    return ptr;

            var uri = $"https://pokeapi.co/api/v2/type/{type}";

            TypeApiData? typeApiData = await httpClient.GetFromJsonAsync<TypeApiData>(uri);

            return new PokemonTypeRelations(typeApiData);
        }
    }
}
