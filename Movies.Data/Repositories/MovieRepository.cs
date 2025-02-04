using Microsoft.EntityFrameworkCore;
using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepository/*(MoviesDbContext _context) */: IMovieRepository
    {
        private readonly MoviesDbContext _context;

        public MovieRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public Movie Add(Movie movie)
        {
            var result = _context.Movies.Add(movie);
            _context.SaveChanges();

            return result.Entity;

        }

        public Movie Delete(int id)
        {
            var result = GetById(id);

            if (result != null)
            {
                _context.Movies.Remove(result);
                _context.SaveChanges();
                return result;
            }

            return null;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.AsNoTracking().ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            var result = GetById(movie.Id);

            if (result != null)
            {
                result.Title = movie.Title;
                result.Genre = movie.Genre;
                result.ReleaseYear = movie.ReleaseYear;

                 _context.SaveChanges();
            }

            return null;
        }
        public IEnumerable<Movie> QueryStringFilter(string titleSearchString, int perPage, string orderBy)
        {
            var listOfMovies = _context.Movies.ToList();

            if (String.IsNullOrWhiteSpace(titleSearchString) == false)
            {
                listOfMovies = listOfMovies.Where(m =>
                m.Title.Contains(titleSearchString, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
            }

            switch (orderBy)
            {
                case "asc":
                    listOfMovies = listOfMovies.OrderBy(m => m.Title).ToList();
                    break;
                case "desc":
                    listOfMovies = listOfMovies.OrderByDescending(m => m.Title).ToList();
                    break;
            }

            if (perPage > 0)
            {
                listOfMovies = listOfMovies.Take(perPage).ToList();
            }

            return listOfMovies;
        }
    }
}
