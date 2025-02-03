using Movies.Data.Models;

namespace Movies.Data.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();

        Movie GetById(int id);

        Movie Add(Movie movie);

        Movie Update(Movie movie);

        Movie Delete(int id);

        IEnumerable<Movie> QueryStringFilter(string titleSearchString, int perPage, string orderBy = "asc");
    }
}
