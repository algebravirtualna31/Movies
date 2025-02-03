using Microsoft.EntityFrameworkCore;

namespace Movies.Data.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :base(options)
        {
                
        }

        public virtual DbSet<Movie> Movies { get; set; }
    }
}
