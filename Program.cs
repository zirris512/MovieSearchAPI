using Microsoft.OpenApi.Models;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using MovieSearch.Controller;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

string? keyVaultName = Environment.GetEnvironmentVariable("VAULT_NAME");
var kvUri = "https://" + keyVaultName + ".vault.azure.net";

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());


var secret = client.GetSecret(Environment.GetEnvironmentVariable("SECRET_NAME"));

var Client = new QueryClient(secret.Value.Value);

builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Search API", Description = "Search for movies and tv shows", Version = "v1" });
  });
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Search API V1");
     });
}

app.MapGet("/movie", (string? query) => Client.GetMovieTitles("movie", query));
app.MapGet("/tv", (string? query) => Client.GetTvTitles("tv", query));
app.MapGet("/person", (string? query) => Client.GetPersonTitles("person", query));

app.Run();
