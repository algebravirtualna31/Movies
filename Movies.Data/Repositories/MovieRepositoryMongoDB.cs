using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepositoryMongoDB : IMovieRepository
    {
        Movie IMovieRepository.Add(Movie movie)
        {
            throw new NotImplementedException();
        }

        Movie IMovieRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Movie> IMovieRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Movie IMovieRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Movie> IMovieRepository.QueryStringFilter(string titleSearchString, int perPage, string orderBy)
        {
            throw new NotImplementedException();
        }

        Movie IMovieRepository.Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
