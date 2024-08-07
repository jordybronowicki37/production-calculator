﻿using ProductionCalculator.Core.components.entities;

namespace ProductionCalculator.Core.components.entityContainer;

public class EntityContainer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
    public virtual ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();
    public virtual ICollection<Machine> Machines { get; private set; } = new List<Machine>();
    
    public EntityContainer() {}

    public Product GetOrGenerateProduct(string name)
    {
        var existingProduct = GetProduct(name);
        if (existingProduct != null) return existingProduct;

        var p = new Product(name);
        Products.Add(p);
        return p;
    }
    
    public Product? GetProduct(string name)
    {
        return Products.FirstOrDefault(r => r.Name == name);
    }
    
    public Product? GetProduct(Guid id)
    {
        return Products.FirstOrDefault(r => r.Id == id);
    }
    
    public void RemoveProduct(string name)
    {
        var product = Products.FirstOrDefault(p => p.Name == name);
        if (product == null) return;
        Products.Remove(product);
    }
    
    public void RemoveProduct(Guid id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return;
        Products.Remove(product);
    }
    
    public Recipe? GetRecipe(string name)
    {
        return Recipes.FirstOrDefault(r => r.Name == name);
    }
    
    public Recipe? GetRecipe(Guid id)
    {
        return Recipes.FirstOrDefault(r => r.Id == id);
    }

    public Recipe GenerateRecipe(string name, Machine machine1, params Machine[] machines)
    {
        if (Recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        var r = new Recipe
        {
            Name = name
        };
        Recipes.Add(r);
        machines = machines.Append(machine1).ToArray();
        foreach (var machine in machines)
        {
            machine.Recipes.Add(r.Id);
        }
        return r;
    }

    public void RemoveRecipe(string name)
    {
        var recipe = Recipes.FirstOrDefault(p => p.Name == name);
        if (recipe == null) return;
        Recipes.Remove(recipe);
    }
    
    public void RemoveRecipe(Guid id)
    {
        var recipe = Recipes.FirstOrDefault(p => p.Id == id);
        if (recipe == null) return;
        Recipes.Remove(recipe);
    }
    
    public RecipeBuilder GetRecipeBuilder(string name, Machine machine1, params Machine[] machines)
    {
        if (Recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        return new RecipeBuilder(this, name, machine1, machines);
    }

    public Machine GenerateMachine(string name)
    {
        var machine = new Machine
        {
            Name = name
        };
        Machines.Add(machine);
        return machine;
    }

    public Machine? GetMachine(string name)
    {
        return Machines.FirstOrDefault(m => m.Name == name);
    }
    
    public Machine? GetMachine(Guid id)
    {
        return Machines.FirstOrDefault(m => m.Id == id);
    }
    
    public IEnumerable<Machine> GetMachines(IEnumerable<Guid> ids)
    {
        return Machines.Where(m => ids.Contains(m.Id));
    }
}