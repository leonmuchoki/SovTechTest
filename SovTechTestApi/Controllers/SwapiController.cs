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
    [Route("api/swapi")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        public const string SWAPI_PEOPLE_URL = "https://swapi.dev/api/people";

        [HttpGet, Route("/people")]
        public async Task<IEnumerable<SwapiPeople>> GetSwapiPeopleAsync()
        {
            List<SwapiPeople> swapiPeople = new List<SwapiPeople>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(SWAPI_PEOPLE_URL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<SwapiPeopleResult>(apiResponse);
                    swapiPeople = res.results;
                    while(res.next != null)
                    {
                        using (var responseNext = await httpClient.GetAsync(res.next))
                        {
                            apiResponse = await responseNext.Content.ReadAsStringAsync();
                            res = JsonConvert.DeserializeObject<SwapiPeopleResult>(apiResponse);
                            swapiPeople = swapiPeople.Concat(res.results).ToList();
                        }
                    }
                }
            }

            return swapiPeople;
        }
    }
}
