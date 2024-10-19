using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<ApplicationDbContext>(connectionString);

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwaggerGen();
}

app.UseDefaultExceptionHandler();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapFallbackToFile("index.html");
app.MapIdentityApi();
app.MapFastEndpoints(config =>
{
    config.Endpoints.RoutePrefix = "api";
    config.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    config.Security.RoleClaimType = ClaimTypes.Role;
});

app.Run();