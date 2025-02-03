using Microsoft.EntityFrameworkCore;
using Movies.Data.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connectionString));                                                                                

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.Run();
    }
}