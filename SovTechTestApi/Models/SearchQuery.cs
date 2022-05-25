using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovTechTestApi.Models
{
    public class SearchQuery
    {
        public string? chuck_query { get; set; }
        public string? swapi_query { get; set; }
    }
    public class SearchChuckResult
    {
        public int? total { get; set; }
        public List<Chuck> result { get; set; }
    }

    public class Chuck
    {
        public string[] categories { get; set; }
        public string created_at { get; set; }
        public string icon_url { get; set; } 
        public string id { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string value { get; set; }
    }

}
