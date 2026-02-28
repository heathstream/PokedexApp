using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class ListApiResponse<T>
    {
        public List<T> Results { get; set; }
    }
}
