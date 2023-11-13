using System.Data;
using Microsoft.EntityFrameworkCore;
using Recipes.Shared.Domain;
using Recipes.Shared.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Recipes.Api.Models;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeDbContext _appDbContext;

    public RecipeRepository(RecipeDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<DtoRecipe>> GetAllRecipes(string filter)
    {
        try
        {
            var recipes = await (from a in _appDbContext.Recipes
                                 join b in _appDbContext.RecipeImages on a.Id equals b.RecipeId into gj1
                                 from c in gj1.DefaultIfEmpty()
                                 where a.Title.Contains(filter) || a.Description.Contains(filter)
                                 select new DtoRecipe
                                 {
                                     Id = a.Id,
                                     Title = a.Title,
                                     Description = a.Description,
                                     PrepTimeMinutes = a.PrepTimeMinutes,
                                     CookTimeMinutes = a.CookTimeMinutes,
                                     Servings = a.Servings,
                                     Image = c == null ? null : c.Image,
                                     Ingredients = _appDbContext.Ingredients.Where(x => x.RecipeId == a.Id).ToList(),
                                     Instructions = _appDbContext.Instructions.Where(x => x.RecipeId == a.Id).ToList()
                                 }).ToListAsync();

            return recipes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<DtoRecipe?> GetRecipe(int id)
    {
        try
        {
            var recipe = await (from a in _appDbContext.Recipes
                                join b in _appDbContext.RecipeImages on a.Id equals b.RecipeId into gj1
                                from c in gj1.DefaultIfEmpty()
                                where a.Id == id
                                select new DtoRecipe
                                {
                                    Id = a.Id,
                                    Title = a.Title,
                                    Description = a.Description,
                                    PrepTimeMinutes = a.PrepTimeMinutes,
                                    CookTimeMinutes = a.CookTimeMinutes,
                                    Servings = a.Servings,
                                    Image = c == null ? null : c.Image,
                                    Ingredients = _appDbContext.Ingredients.Where(x => x.RecipeId == a.Id).ToList(),
                                    Instructions = _appDbContext.Instructions.Where(x => x.RecipeId == a.Id).ToList()
                                }).SingleOrDefaultAsync();

            return recipe;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CreateRecipe(DtoRecipe recipe)
    {
        try
        {
            // Recipe
            var newRecipe = new Recipe
            {
                Title = recipe.Title,
                Description = recipe.Description,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                Servings = recipe.Servings
            };
            await _appDbContext.Recipes.AddAsync(newRecipe);
            await _appDbContext.SaveChangesAsync();

            // Image
            await _appDbContext.AddAsync(new RecipeImage { RecipeId = recipe.Id, Image = recipe.Image });
            await _appDbContext.SaveChangesAsync();

            // Ingredients
            recipe.Ingredients.ForEach(x => x.RecipeId = newRecipe.Id);
            await _appDbContext.Ingredients.AddRangeAsync(recipe.Ingredients);
            await _appDbContext.SaveChangesAsync();

            // Instructions
            recipe.Instructions.ForEach(x => x.RecipeId = newRecipe.Id);
            await _appDbContext.Instructions.AddRangeAsync(recipe.Instructions);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateRecipe(DtoRecipe recipe)
    {
        try
        {
            var existing = await _appDbContext.Recipes.FindAsync(recipe.Id) ?? throw new DataException($"Recipe {recipe.Id} not found");

            // Recipe
            _appDbContext.Entry(existing).CurrentValues.SetValues(recipe);
            _appDbContext.Update(existing);
            await _appDbContext.SaveChangesAsync();

            // Image
            var image = _appDbContext.RecipeImages.FirstOrDefault(x => x.RecipeId == recipe.Id);
            if (image != null)
            {
                image.Image = recipe.Image;
                _appDbContext.Update(image);
            }
            else
            {
                await _appDbContext.AddAsync(new RecipeImage { RecipeId = recipe.Id, Image = recipe.Image });
            }
            await _appDbContext.SaveChangesAsync();

            // Ingredients
            _appDbContext.Ingredients.RemoveRange(_appDbContext.Ingredients.Where(x => x.RecipeId == recipe.Id));
            recipe.Ingredients.ForEach(x => x.RecipeId = existing.Id);
            await _appDbContext.Ingredients.AddRangeAsync(recipe.Ingredients);
            await _appDbContext.SaveChangesAsync();

            // Instructions
            _appDbContext.Instructions.RemoveRange(_appDbContext.Instructions.Where(x => x.RecipeId == recipe.Id));
            recipe.Instructions.ForEach(x => x.RecipeId = existing.Id);
            await _appDbContext.Instructions.AddRangeAsync(recipe.Instructions);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}