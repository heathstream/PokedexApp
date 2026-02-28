using Android.AdServices.Common;
using PokedexApp.Helpers;
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
        const int NO_OF_POKEMON = 1350;
        const int NO_OF_ITEMS = 100000;
        const int NO_OF_MOVES = 100000;

        HttpClient httpClient = new HttpClient();

        Dictionary<string, EvolutionChain> _evolutionChainCache = new();
        Dictionary<string, Pokemon> _pokemonCache = new();
        Dictionary<string, Item> _itemCache = new();
        Dictionary<string, Move> _moveCache = new();

        List<PokemonListItem> _pkmnListItemCache = new();
        List<ItemListItem> _itemListItemCache = new();
        List<MoveListItem> _moveListItemCache = new();

        public PokeApiService()
        {
            Task.Run(GetTypeRelationsAsync);
        }

        //public async Task<List<Pokemon>> GetAllPokemonAsync()
        //{
        //    var pokemon = new List<Pokemon>();
        //    for (int i = 1; i <= NO_OF_POKEMON; i++)
        //        pokemon.Add(await GetPokemonAsync(i.ToString()));
        //    return pokemon;
        //}

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (_pokemonCache.TryGetValue(name.ToLower(), out Pokemon? cachedPokemon))
                return cachedPokemon;

            // Load Pokemon data from the Pokemon and Species endpoints
            var uri = $"https://pokeapi.co/api/v2/pokemon/{name}";
            PokemonApiData? pokemonApiData = await httpClient.GetFromJsonAsync<PokemonApiData>(uri);
            uri = $"https://pokeapi.co/api/v2/pokemon-species/{name}";
            SpeciesApiData? speciesApiData = await httpClient.GetFromJsonAsync<SpeciesApiData>(uri);
            if (pokemonApiData == null || speciesApiData == null)
                throw new Exception("Failed to retrieve Pokémon data.");

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
            ListApiResponse<PokemonListItem>? apiResponse = await httpClient.GetFromJsonAsync<ListApiResponse<PokemonListItem>>(uri);
            if (apiResponse == null)
                throw new Exception("Failed to retrieve Pokémon list items.");

            var listItems = apiResponse.Results;
            _pkmnListItemCache.AddRange(listItems);
            return listItems;
        }

        public async Task<List<ItemListItem>> GetItemListItemsAsync()
        {
            if (_itemListItemCache.Any())
                return _itemListItemCache;

            var uri = $"https://pokeapi.co/api/v2/item?limit={NO_OF_ITEMS}";
            ListApiResponse<ItemListItem>? apiResponse = await httpClient.GetFromJsonAsync<ListApiResponse<ItemListItem>>(uri);
            if (apiResponse == null)
                throw new Exception("Failed to retrieve Item list items.");

            var listItems = apiResponse.Results;
            _itemListItemCache.AddRange(listItems);
            return listItems;
        }

        public async Task<List<MoveListItem>> GetMoveListItemsAsync()
        {
            if (_itemListItemCache.Any())
                return _moveListItemCache;

            var uri = $"https://pokeapi.co/api/v2/move?limit={NO_OF_MOVES}";
            ListApiResponse<MoveListItem>? apiResponse = await httpClient.GetFromJsonAsync<ListApiResponse<MoveListItem>>(uri);
            if (apiResponse == null)
                throw new Exception("Failed to retrieve Item list items.");

            var listItems = apiResponse.Results;
            _moveListItemCache.AddRange(listItems);
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
            if (apiData == null)
                throw new Exception("Failed to retrieve EvolutionChain data.");

            var evolutionChain = new EvolutionChain()
            {
                Id = apiData.id,
                Chain = await Convert(apiData.chain)
            };

            _evolutionChainCache[pokemon.Name.ToLower()] = evolutionChain;
            return evolutionChain;

            async Task<Evolution> Convert(chainlink apiData) => new Evolution()
                {
                    Pokemon = await GetPokemonAsync(apiData.species.name),
                    EvolvesTo = (await Task.WhenAll(apiData.evolves_to.Select(Convert))).ToList(),
                    Details = new EvolutionDetails()
                    {
                        Item = await GetItemAsync(apiData.evolution_details.FirstOrDefault()?.item?.name),
                        MinLevel = apiData.evolution_details.FirstOrDefault()?.min_level,
                    }
                };
        }

        public async Task<Item?> GetItemAsync(string? itemName)
        {
            if (itemName == null) return null;
            if (_itemCache.TryGetValue(itemName, out Item? cachedItem))
                return cachedItem;

            var uri = $"https://pokeapi.co/api/v2/item/{itemName}";
            ItemApiData? apiData = await httpClient.GetFromJsonAsync<ItemApiData>(uri);
            if (apiData == null)
                throw new Exception("Failed to retrieve Item data.");

            var item = new Item()
            {
                Name = StringHelper.CleanName(apiData.name),
                Cost = apiData.cost,
                FlavorText = apiData.flavor_text_entries.Last(ft => ft.language.name == "en").flavor_text,
                Category = Enum.Parse<ItemCategory>(apiData.category.name.Replace("-", "_"), true)
            };

            _itemCache[item.Name.ToLower()] = item;
            return item;
        }

        public async Task<Move> GetMoveAsync(string? moveName)
        {
            if (moveName == null) return null;
            if (_moveCache.TryGetValue(moveName, out Move? cachedMove))
                return cachedMove;

            var uri = $"https://pokeapi.co/api/v2/move/{moveName}";
            MoveApiData? apiData = await httpClient.GetFromJsonAsync<MoveApiData>(uri);
            if (apiData == null)
                throw new Exception("Failed to retrieve Move data.");

            var move = new Move(apiData);

            _moveCache[move.Name.ToLower()] = move; 
            return move;
        }
    }
}
