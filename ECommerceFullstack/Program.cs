using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<ApplicationDbContext>(connectionString);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationJwtBearer(config => config.SigningKey = builder.Configuration["Authentication:PrivateKey"]);

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

app.MapFallbackToFile("index.html");
app.MapFastEndpoints(config =>
{
    config.Endpoints.RoutePrefix = "api";
    config.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    config.Security.RoleClaimType = ClaimTypes.Role;
});

app.Run();