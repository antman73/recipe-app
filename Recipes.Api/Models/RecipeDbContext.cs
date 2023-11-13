using Microsoft.EntityFrameworkCore;
using Recipes.Shared.Domain;

namespace Recipes.Api.Models;

public class RecipeDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeImage> RecipeImages { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Instruction> Instructions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().HasData(
            new { Id = 1, Title = "Recipe 1", Description = "Recipe 1 desc", PrepTimeMinutes = 15, CookTimeMinutes = 10, Servings = 4 },
            new { Id = 2, Title = "Recipe 2", Description = "Recipe 2 desc", PrepTimeMinutes = 30, CookTimeMinutes = 20, Servings = 6 },
            new { Id = 3, Title = "Recipe 3", Description = "Recipe 3 desc", PrepTimeMinutes = 45, CookTimeMinutes = 30, Servings = 8 },
            new { Id = 4, Title = "Recipe 4", Description = "Recipe 4 desc", PrepTimeMinutes = 60, CookTimeMinutes = 40, Servings = 10 }
        );

        base.OnModelCreating(modelBuilder);
    }
}