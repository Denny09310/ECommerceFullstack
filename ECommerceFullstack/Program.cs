using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Security.Claims;

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
    app.UseSwaggerGen();
}

app.UseDefaultExceptionHandler();

app.UseHttpsRedirection();

app.MapFastEndpoints(config =>
{
    config.Security.RoleClaimType = ClaimTypes.Role;
});

app.Run();