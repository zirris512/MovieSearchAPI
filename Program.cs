using Microsoft.OpenApi.Models;
using MovieSearch.Controller;


var builder = WebApplication.CreateBuilder(args);
var movieApiKey = builder.Configuration["Movies:ApiKey"]!;

var Client = new QueryClient(movieApiKey);

builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Search API", Description = "Search for movies and tv shows", Version = "v1" });
  });
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Search API V1");
     });
}
else if (app.Environment.IsProduction())
{
    Client = new QueryClient(movieApiKey);
}


app.MapGet("/movie", (string query) => Client.GetMovieTitles("movie", query));
app.MapGet("/tv", (string query) => Client.GetTvTitles("tv", query));
app.MapGet("/person", (string query) => Client.GetPersonTitles("person", query));

app.Run();
