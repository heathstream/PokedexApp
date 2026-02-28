using PokedexApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokedexApp.Models
{
    public abstract class ListItem
    {
        public abstract string Name { get; set; }
        public abstract string DisplayName { get; set; }
    }

    public class ItemListItem : ListItem
    {
        public override string Name { get; set; }
        public override string DisplayName { get; set; }
    }

    public class MoveListItem : ListItem
    {
        public override string Name { get; set; }
        public override string DisplayName { get; set; }
    }

    public class PokemonListItem : ListItem, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        List<PokemonType>? _types;
        int? _id;

        public override string Name { get; set; }
        public override string DisplayName { get; set; }
        public PokemonType? FirstType => Types?[0];
        public PokemonType? SecondType => HasTwoTypes ? Types?[1] : null;
        public bool HasTwoTypes => Types?.Count > 1;
        public bool HasOneType => !HasTwoTypes;
        public bool HasId => Id != null;

        public List<PokemonType>? Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
                OnPropertyChanged(nameof(FirstType));
                OnPropertyChanged(nameof(SecondType));
                OnPropertyChanged(nameof(HasTwoTypes));
                OnPropertyChanged(nameof(HasOneType));
            }
        }
        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(HasId));
            }
        }
        public string Url { get; set; }

        public PokemonListItem(string name, string url)
        {
            Name = name;
            DisplayName = StringHelper.CleanName(name);
            Url = url;
        }

        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
