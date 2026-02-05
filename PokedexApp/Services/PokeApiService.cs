using PokedexApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokedexApp.Services
{
    internal class PokeApiService
    {
        const int NO_OF_POKEMON = 151;

        HttpClient httpClient;

        Dictionary<string, Pokemon> _pokemonCache = new();
        List<PokemonListItem> _pkmnListItemCache = new();
        Dictionary<string, EvolutionChain> _evolutionChainCache = new();

        public PokeApiService()
        {
            httpClient = new HttpClient();
            Task.Run(GetTypeRelationsAsync);
            for (int i = 1; i <= NO_OF_POKEMON; i++)
            {
                var t = Task.Run(() => GetPokemonAsync(i.ToString()));
                t.Wait();
            }
            var pokemon = Task.Run(() => GetPokemonAsync("pikachu")).Result;
            var evolutionChain = Task.Run(() => GetEvolutionChainAsync(pokemon)).Result;
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
            _pokemonCache[pokemon.Name.ToLower()] = pokemon;

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

        public async Task<EvolutionChain> GetEvolutionChainAsync(Pokemon pokemon)
        {
            if (_evolutionChainCache.TryGetValue(pokemon.Name.ToLower(), out EvolutionChain? cached))
                return cached;

            var uri = pokemon.EvolutionChainUrl;
            EvolutionChainApiData? apiData = await httpClient.GetFromJsonAsync<EvolutionChainApiData>(uri);

            var evolutionChain = new EvolutionChain()
            {
                Id = apiData.id,
                Chain = await Convert(apiData.chain)
            };

            _evolutionChainCache[pokemon.Name.ToLower()] = evolutionChain;
            return evolutionChain;

            async Task<ChainLink> Convert(chainlink apiData) => new ChainLink()
                {
                    Pokemon = await GetPokemonAsync(apiData.species.name),
                    MinLevel = apiData.evolution_details.FirstOrDefault()?.min_level,
                    EvolvesTo = (await Task.WhenAll(apiData.evolves_to.Select(Convert))).ToList()
                };
        }
    }
}
