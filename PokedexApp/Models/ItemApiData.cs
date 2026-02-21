using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class ItemApiData
    {
        public string name { get; set; }
        public int cost { get; set; }
        public List<flavor_text_entry> flavor_text_entries { get; set; }
        public NamedApiResource category { get; set; }
    }
}
