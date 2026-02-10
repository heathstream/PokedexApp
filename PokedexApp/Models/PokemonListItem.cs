using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokedexApp.Models
{
    public class PokemonListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        List<PokemonType>? _types;
        int? _id;

        public List<PokemonType>? Types
        { 
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }
        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name { get; set; }
        public string Url { get; set; }

        public PokemonListItem(string name, string url)
        {
            Name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            Url = url;
        }

        public void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
