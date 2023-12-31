﻿using Microsoft.EntityFrameworkCore;
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
        modelBuilder.Entity<Recipe>().Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<RecipeImage>().Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Ingredient>().Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Instruction>().Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}