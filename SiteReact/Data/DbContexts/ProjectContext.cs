using Microsoft.EntityFrameworkCore;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data.DbContexts;

public class ProjectContext: DbContext
{
    public DbSet<Worksheet> Worksheets { get; set; } = null!;
    public DbSet<EntityContainer> EntityContainers { get; set; } = null!;

    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worksheet>().ToTable("worksheet");
        modelBuilder.Entity<Worksheet>().HasOne(w => w.EntityContainer).WithMany();
        modelBuilder.Entity<Worksheet>().HasMany(w => w.Nodes).WithOne();
        modelBuilder.Entity<Worksheet>().Ignore(w => w.CalculationSucceeded);
        modelBuilder.Entity<Worksheet>().Ignore(w => w.CalculationError);

        // Nodes
        modelBuilder.Entity<ANode>().ToTable("node");
        modelBuilder.Entity<ANode>().HasMany(n => n.ProductionTargets).WithOne().HasForeignKey("NodeId");
        modelBuilder.Entity<ANode>().HasDiscriminator<string>("Node_type")
            .HasValue<SpawnNode>("Spawn")
            .HasValue<ProductionNode>("Production")
            .HasValue<EndNode>("End");
        
        modelBuilder.Entity<SpawnNode>().Property(n => n.Amount).HasColumnName("Amount");
        modelBuilder.Entity<SpawnNode>().HasOne(n => n.Product).WithMany().HasForeignKey("ProductId");
        modelBuilder.Entity<SpawnNode>().Property("ProductId").HasColumnName("ProductId");
        
        modelBuilder.Entity<ProductionNode>().Property(n => n.ProductionAmount).HasColumnName("Amount");
        modelBuilder.Entity<ProductionNode>().HasOne(n => n.Recipe).WithMany().HasForeignKey("RecipeId");

        modelBuilder.Entity<EndNode>().Property(n => n.Amount).HasColumnName("Amount");
        modelBuilder.Entity<EndNode>().HasOne(n => n.Product).WithMany().HasForeignKey("ProductId");
        modelBuilder.Entity<EndNode>().Property("ProductId").HasColumnName("ProductId");
        
        modelBuilder.Entity<Connection>().ToTable("connection");

        // Entities
        modelBuilder.Entity<EntityContainer>().ToTable("entity_container");
        modelBuilder.Entity<EntityContainer>().HasMany(c => c.Recipes).WithOne();
        modelBuilder.Entity<EntityContainer>().HasMany(c => c.Products).WithOne();
        
        modelBuilder.Entity<Product>().ToTable("product");
        
        modelBuilder.Entity<Recipe>().ToTable("recipe");
        modelBuilder.Entity<Recipe>().Property(r => r.Name).HasColumnName("Name");
        modelBuilder.Entity<Recipe>().HasMany(r => r.InputThroughPuts).WithOne().HasForeignKey("RecipeInputId");
        modelBuilder.Entity<Recipe>().HasMany(r => r.OutputThroughPuts).WithOne().HasForeignKey("RecipeOutputId");
        
        modelBuilder.Entity<ThroughPut>().ToTable("throughput");
        
        modelBuilder.Entity<TargetProduction>().ToTable("target_production");
        modelBuilder.Entity<TargetProduction>().Property(t => t.Amount).HasColumnName("Amount");
        modelBuilder.Entity<TargetProduction>().Property(t => t.Type).HasColumnName("Type");
        
        modelBuilder.Entity<TargetConnection>().ToTable("target_connection");
        modelBuilder.Entity<TargetConnection>().Property(t => t.Amount).HasColumnName("Amount");
        modelBuilder.Entity<TargetConnection>().Property(t => t.Type).HasColumnName("Type");
        
        base.OnModelCreating(modelBuilder);
    }
}