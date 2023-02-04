using Microsoft.EntityFrameworkCore;
using SiteReact.Data.DbContexts;
using SiteReact.Data.Initializers;

var builder = WebApplication.CreateBuilder(args);

const string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _myAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoConnectionString = Environment.GetEnvironmentVariable("MongoConnectionString") ?? throw 
    new ArgumentNullException($"Environment variable not set: MongoConnectionString");
builder.Services.AddSingleton(new DocumentContext(mongoConnectionString));

builder.Services.AddDbContext<MainContext>(context =>
{
    var postgresConnectionString = Environment.GetEnvironmentVariable("PostgreSQLConnectionString") ?? throw 
        new ArgumentNullException($"Environment variable not set: PostgreSQLConnectionString");
    context.UseLazyLoadingProxies().UseNpgsql(postgresConnectionString, 
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MainContext>();

    if (app.Environment.IsDevelopment())
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        // TestDataInitializer.InitializeAllData(context);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(_myAllowSpecificOrigins);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();