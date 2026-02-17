using PokedexApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokedexApp.Services
{
    public class PokeApiService
    {
        const int NO_OF_POKEMON = 1000;

        HttpClient httpClient;

        Dictionary<string, Pokemon> _pokemonCache = new();
        Dictionary<string, EvolutionChain> _evolutionChainCache = new();
        Dictionary<string, Move> _moveCache = new();
        List<PokemonListItem> _pkmnListItemCache = new();

        public PokeApiService()
        {
            httpClient = new HttpClient();
            Task.Run(GetTypeRelationsAsync);
        }

        public async Task<List<Pokemon>> GetAllPokemonAsync()
        {
            var pokemon = new List<Pokemon>();
            for (int i = 1; i <= NO_OF_POKEMON; i++)
                pokemon.Add(await GetPokemonAsync(i.ToString()));
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (_pokemonCache.TryGetValue(name.ToLower(), out Pokemon? cachedPokemon))
                return cachedPokemon;

            // Load Pokemon data from the Pokemon and Species endpoints
            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
            PokemonApiData? pokemonApiData = await httpClient.GetFromJsonAsync<PokemonApiData>(uri);
            uri = $"https://pokeapi.co/api/v2/pokemon-species/{name}";
            SpeciesApiData? speciesApiData = await httpClient.GetFromJsonAsync<SpeciesApiData>(uri);

            // Create the Pokemon object and cache it for next time
            var pokemon = new Pokemon(pokemonApiData, speciesApiData);
            _pokemonCache[pokemon.Name.ToLower()] = pokemon;

            // Update the Pokemon's list item with additional info
            var cachedListItem = _pkmnListItemCache.Find(p => p.Name == pokemon.Name);
            if (cachedListItem != null)
            {
                cachedListItem.Id = pokemon.Id;
                cachedListItem.Types = pokemon.Types;
            }    

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
                    TypeRelations.Chart[type][ParseType(other.name)] = 2.0f;
                foreach (var other in typeApiData.damage_relations.half_damage_from)
                    TypeRelations.Chart[type][ParseType(other.name)] = 0.5f;
                foreach (var other in typeApiData.damage_relations.no_damage_from)
                    TypeRelations.Chart[type][ParseType(other.name)] = 0.0f;
            }

            PokemonType ParseType(string typeName) => Enum.Parse<PokemonType>(typeName, true);
        }

        public async Task<EvolutionChain> GetEvolutionChainAsync(Pokemon pokemon)
        {
            if (_evolutionChainCache.TryGetValue(pokemon.EvolutionChainUrl, out EvolutionChain? cached))
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

        public async Task<Move> GetMoveAsync(string moveName)
        {
            if (_moveCache.TryGetValue(moveName, out Move? cachedMove))
                return cachedMove;

            var uri = $"https://pokeapi.co/api/v2/move/{moveName}";
            MoveApiData? apiData = await httpClient.GetFromJsonAsync<MoveApiData>(uri);

            var move = new Move(apiData);

            _moveCache[move.Name.ToLower()] = move; 
            return move;
        }
    }
}
