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
        List<PokemonListItem> _pkmnListItemCache = new();

        public PokeApiService()
        {
            httpClient = new HttpClient();
            var t = Task.Run(() => GetTypeRelationsAsync());
            t.Wait();
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

        public async Task GetTypeRelationsAsync()
        {
            foreach (var type in Enum.GetValues<PokemonType>())
            {
                var uri = $"https://pokeapi.co/api/v2/type/{type.ToString()}";
                TypeApiData? typeApiData = await httpClient.GetFromJsonAsync<TypeApiData>(uri);

                foreach (var other in typeApiData.damage_relations.double_damage_from)
                    TypeRelations.Chart[type][Enum.Parse<PokemonType>(other.name, true)] = 2.0f;
                foreach (var other in typeApiData.damage_relations.half_damage_from)
                    TypeRelations.Chart[type][Enum.Parse<PokemonType>(other.name, true)] = 0.5f;
                foreach (var other in typeApiData.damage_relations.no_damage_from)
                    TypeRelations.Chart[type][Enum.Parse<PokemonType>(other.name, true)] = 0.0f;
            }
        }
    }
}
