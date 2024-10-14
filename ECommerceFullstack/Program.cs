using FastEndpoints;
using FastEndpoints.Swagger;
using FastEndpoints.Security;

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

app.MapFastEndpoints();

app.Run();