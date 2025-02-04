using Microsoft.AspNetCore.Mvc;
using Movies.Controllers;
using Movies.Data.Models;
using Movies.Data.Repositories;

namespace Movies.Tests
{
    public class MoviesControllerTests
    {

        private readonly MoviesDbContext _context;
        private readonly MovieRepository _repository;
        private readonly MoviesController _moviesController;

        public MoviesControllerTests()
        {
            _context = new MoviesDbContext();
            _repository = new MovieRepository(_context);
            _moviesController = new MoviesController(_repository);
        }

        [Fact]
        public void GetAllMovies_ReturnSuccessIfCorrectCount()
        {
            //Arrange
            //Act
            var result = _moviesController.GetMovies();

            //Assert

            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Movie>>(list.Value);

            var listMovies = list.Value as List<Movie>;

            Assert.Equal(100, listMovies.Count);
        }

        [Fact]
        public void GetAllMovies_ReturnSuccessIfWrongCount()
        {
            //Arrange
            //Act
            var result = _moviesController.GetMovies();

            //Assert

            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Movie>>(list.Value);

            var listMovies = list.Value as List<Movie>;

            Assert.NotEqual(5, listMovies.Count);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(10)]
        public void GetMovieById_ReturnsOkObjectResultIfFound(int id)
        {
            var okResult = _moviesController.GetMovie(id);
  
            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;

            Assert.IsType<Movie>(item.Value);

            var movie = item.Value as Movie;

            Assert.Equal(id, movie.Id);

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(61616161616161)]
        [InlineData(-13231)]
        [InlineData(0)]
        public void GetMovieById_ReturnsNotFoundObjectIfNotFound(int id)
        {
            var notFoundResult = _moviesController.GetMovie(id);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);

        }

        [Theory]
        [InlineData(1)]

        public void RemovMovieById_ReturnsOkObjectResultIfDeletedSuccessfully(int id)
        {
            var okResult = _moviesController.DeleteMovie(id);

            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;

            Assert.IsType<Movie>(item.Value);

            var movie = item.Value as Movie;

            Assert.Equal(id, movie.Id);

        }

        [Theory]
        [InlineData(-1000)]

        public void RemovMovieById_ReturnsNotFoundIfNotExists(int id)
        {
            var notFoundResult = _moviesController.DeleteMovie(id);

            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);

            var notFoundObjectResult = notFoundResult.Result as NotFoundObjectResult;

            var notFoundMessage = notFoundObjectResult.Value.ToString();

            Assert.Equal(notFoundMessage, $"Movie with Id = {id} not found");
        }
    }
}