using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiteReact.Data.DbContexts;
using SiteReact.Data.Initializers;
using SiteReact.Security;

namespace SiteReact;

public class Startup: StartupBase
{
    private readonly IConfiguration _configuration;
    private const string AllowEverythingPolicyName = "_AllowEverything";
    private const string AdminOnlyPolicyName = "_AdminOnly";

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public override void Configure(IApplicationBuilder app)
    {
        var webapp = (WebApplication) app;
        
        // Configure the HTTP request pipeline.
        if (!webapp.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            webapp.UseHsts();
        }
        else
        {
            webapp.UseSwagger();
            webapp.UseSwaggerUI();
            webapp.UseDeveloperExceptionPage();
        }

        using (var scope = webapp.Services.CreateScope())
        {
            UserRolesInitializer.CreateRoles(scope.ServiceProvider).Wait();
            if (webapp.Environment.IsDevelopment())
            {
                var serviceProvider = scope.ServiceProvider;
                var documentContext = serviceProvider.GetRequiredService<DocumentContext>();
                TestDataInitializer.InitializeAllData(documentContext);
            }
        }

        webapp.UseHttpsRedirection();
        webapp.UseStaticFiles();
        webapp.UseRouting();
        webapp.UseCors(AllowEverythingPolicyName);

        webapp.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        webapp.MapFallbackToFile("index.html");
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddCors(AddAllowEverythingPolicy);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddSingleton(new DocumentContext(_configuration.GetConnectionString("MongoConnectionString")));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var mySqlConnectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString));
        });
        
        ConfigureSecurity(services);
    }

    private void ConfigureSecurity(IServiceCollection services)
    {
        services.AddScoped<JWTGenerator>();
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };
            });

        services.AddAuthorization(AddAdminOnlyPolicy);
    }
    
    private static void AddAllowEverythingPolicy(CorsOptions options)
    {
        options.AddPolicy(name: AllowEverythingPolicyName,
            policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
    }

    private static void AddAdminOnlyPolicy(AuthorizationOptions options)
    {
        options.AddPolicy(AdminOnlyPolicyName, policy => policy.RequireRole("Admin"));
    }
}