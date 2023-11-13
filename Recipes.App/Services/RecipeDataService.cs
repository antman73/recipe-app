using Newtonsoft.Json;
using Recipes.Shared.Entities;
using System.Net.Http.Json;
using System.Text;

namespace Recipes.App.Services;

public class RecipeDataService : IRecipeDataService
{
    private readonly HttpClient _httpClient;

    public RecipeDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DtoRecipe>> GetAllRecipes()
    {
        var result = await _httpClient.GetAsync("api/recipes");
        if (!result.IsSuccessStatusCode) return new List<DtoRecipe>();
        return await result.Content.ReadFromJsonAsync<List<DtoRecipe>>() ?? new List<DtoRecipe>();
    }

    public async Task<IEnumerable<DtoRecipe>> GetRecipes(string? filter = " ")
    {
        var result = await _httpClient.GetAsync($"api/recipes/{filter}");
        if (!result.IsSuccessStatusCode) return new List<DtoRecipe>();
        return await result.Content.ReadFromJsonAsync<List<DtoRecipe>>() ?? new List<DtoRecipe>();
    }

    public async Task<DtoRecipe?> GetRecipe(int id)
    {
        var result = await _httpClient.GetAsync($"api/recipes/{id}");
        if (!result.IsSuccessStatusCode) return new DtoRecipe();
        return await result.Content.ReadFromJsonAsync<DtoRecipe?>();
    }

    public async Task<bool> CreateRecipe(DtoRecipe recipe)
    {
        var json = JsonConvert.SerializeObject(recipe);
        var result = await _httpClient.PostAsync("api/recipes",
            new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode) return false;
        return await result.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> UpdateRecipe(DtoRecipe recipe)
    {
        var json = JsonConvert.SerializeObject(recipe);
        var result = await _httpClient.PutAsync("api/recipes",
            new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode) return false;
        return await result.Content.ReadFromJsonAsync<bool>();
    }
}