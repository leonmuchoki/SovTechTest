using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SovTechTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SovTechTestApi.Controllers
{
    [Route("api/chuck")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        public const string CHUCK_CATEGORIES_URL = "https://api.chucknorris.io/jokes/categories";
        public const string CHUCK_CATEGORY_URL = "https://api.chucknorris.io/jokes/random?category=";

        [HttpGet, Route("/categories")]
        public async Task<IEnumerable<string>> GetChuckCategoriesAsync()
        {
            List<string> chuckCategories = new List<string>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CHUCK_CATEGORIES_URL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    chuckCategories = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }

            return chuckCategories;
        }

        [HttpGet("{category}")]
        public async Task<ChuckCategory> GetChuckCategoryAsync(string category)
        {
            var chuckCategory = new ChuckCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(CHUCK_CATEGORY_URL + category.Trim()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    chuckCategory = JsonConvert.DeserializeObject<ChuckCategory>(apiResponse);
                }
            }

            return chuckCategory;
        }

    }
}
