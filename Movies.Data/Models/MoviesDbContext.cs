using Microsoft.EntityFrameworkCore;

namespace Movies.Data.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :base(options)
        {
                
        }

        public MoviesDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Movies;Trusted_Connection=True;Encrypt=False;");
            }
        }

        public virtual DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .ToTable("Movie");

            //modelBuilder.Entity<Movie>()
            //    .Property(p => p.Id)
            //    .ValueGeneratedOnAdd();

        }
    }
}
