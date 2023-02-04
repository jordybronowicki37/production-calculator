using Microsoft.EntityFrameworkCore;

namespace SiteReact.Data.DbContexts;

public class MainContext: DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}