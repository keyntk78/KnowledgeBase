using FluentValidation.AspNetCore;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Stores;
using KnowledgeBase.BackendServer.Data;
using KnowledgeBase.BackendServer.Data.Entities;
using KnowledgeBase.BackendServer.IdentityServer;
using KnowledgeBase.BackendServer.Services;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


/*Add Services*/

//1. Setup entity framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//2. Setup idetntity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
 .AddInMemoryApiResources(Config.Apis)
 .AddInMemoryApiScopes(Config.ApiScopes)
 .AddInMemoryClients(Config.Clients)
 .AddInMemoryIdentityResources(Config.Ids)
 .AddAspNetIdentity<User>()
 .AddDeveloperSigningCredential();


// .AddProfileService<IdentityProfileService>()

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
});



builder.Services.AddControllersWithViews().AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<RoleCreateRequestValidator>());

builder.Services.AddAuthentication()
         .AddLocalApi("Bearer", option =>
         {
             option.ExpectedScope = "api.knowledgebase";
         });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
    {
        foreach (var selector in model.Selectors)
        {
            var attributeRouteModel = selector.AttributeRouteModel;
            attributeRouteModel.Order = -1;
            attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
        }
    });
});


builder.Services.AddTransient<DbInitializer>(); 
builder.Services.AddTransient<IEmailSender, EmailSenderService>();
builder.Services.AddTransient<ISequenceService, SequenceService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Knowledge Base API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://localhost:5000/connect/authorize"),
                    Scopes = new Dictionary<string, string> { { "api.knowledgebase", "KnowledgeBase API" } }
                },
            },
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            new List<string>{ "api.knowledgebase" }
                        }
                    });
    });

var app = builder.Build();

//Serilog
Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .CreateLogger();


// seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Log.Information("Seeding data...");
        var dbInitializer = services.GetService<DbInitializer>();
        dbInitializer.Seed().Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("swagger");
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Knowledge Base API V1");
    });
}

app.UseStaticFiles();
app.UseIdentityServer();

app.UseAuthentication(); ;

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
