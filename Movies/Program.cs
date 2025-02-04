using Microsoft.EntityFrameworkCore;
using Movies.Data.Interfaces;
using Movies.Data.Models;
using Movies.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(
    s => s.SwaggerDoc("v1",
    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Movies.API", Version = "v1" }));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies.API v1"));

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.Run();
