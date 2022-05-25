using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SovTechTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SovTechTestApi.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public const string CHUCK_SEARCH_URL = "https://api.chucknorris.io/jokes/search?query=";
        public const string SWAPI_SEARCH_URL = "https://swapi.dev/api/people/?search=";

        [HttpGet]
        public async Task<Dictionary<string, dynamic>> Get([FromQuery] SearchQuery query)
        {
            var queryResult = new Dictionary<string, dynamic>();
            using (var httpClient = new HttpClient())
            {
                if(query.chuck_query != null)
                {
                    using (var response = await httpClient.GetAsync(CHUCK_SEARCH_URL + query.chuck_query.Trim()))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<SearchChuckResult>(apiResponse);
                        queryResult.Add("chuck", res);
                    }
                }

                if (query.swapi_query != null)
                {
                    using (var response = await httpClient.GetAsync(SWAPI_SEARCH_URL + query.swapi_query.Trim()))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<SwapiPeopleResult>(apiResponse);
                        queryResult.Add("swapi", res);
                    }
                }

            }

            return queryResult;
        }

    }
}
