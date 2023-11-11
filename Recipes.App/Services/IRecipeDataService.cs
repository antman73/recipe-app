using Newtonsoft.Json;
using Recipes.Shared.Domain;
using System.Net.Http.Json;
using System.Text;

namespace Recipes.App.Services
{
    public interface IRecipeDataService
    {
        public Task<IEnumerable<Recipe>> GetAllRecipes();

        public Task<Recipe> GetRecipe(int id);
    }

    public class RecipeDataService : IRecipeDataService
    {
        private readonly HttpClient _httpClient;

        public RecipeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            var result = await _httpClient.GetAsync("api/recipes");
            if(result.IsSuccessStatusCode) 
                return await result.Content.ReadFromJsonAsync<IEnumerable<Recipe>>() ?? new List<Recipe>();
            return new List<Recipe>();
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            var result = await _httpClient.GetAsync($"api/recipe/{id}");
            if(result.IsSuccessStatusCode)
                return await result.Content.ReadFromJsonAsync<Recipe>() ?? new Recipe();
            return new Recipe();
        }

        public async Task<bool> CreateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PostAsync("api/recipe",
                new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadFromJsonAsync<bool>();
            return false;
        }
    }
}