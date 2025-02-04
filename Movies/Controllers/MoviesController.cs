using Microsoft.AspNetCore.Mvc;
using Movies.Data.Interfaces;
using Movies.Data.Models;


namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("getMovies")]
        //[Route("[action]")]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            try
            {
                var allMovies = _movieRepository.GetAll();

                return Ok(allMovies);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        //[Route("[action]/{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult PostMovie([FromBody] Movie movie)
        {

            try
            {
                var createdMovie = _movieRepository.Add(movie);

                return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, movie);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error createing movie record");
            }

        }


        [HttpDelete("{id}")]
        //[Route("[action]/{id}")]
        public ActionResult<Movie> DeleteMovie(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null)
            {
                return NotFound($"Movie with Id = {id} not found");
            }

            var deletedMovie = _movieRepository.Delete(id);

            return Ok(deletedMovie);
        }


        [HttpPut("{id}")]
        public ActionResult PutMovie(int id, Movie movie)
        {

            try
            {
                if(id != movie.Id)
                {
                    return BadRequest("Movie ID mismatch");
                }

                var movieToUpdate = _movieRepository.GetById(id);

                if (movieToUpdate == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }

                var updatedMovie = _movieRepository.Update(movie);  


                return Ok(updatedMovie);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // GET: api/Movies/search
        [HttpGet("search")]
        public ActionResult SearchByQueryString([FromQuery] string titleSearchString, [FromQuery] int perPage = 0, [FromQuery] string orderBy = "asc")
        {
            try
            {
                var movies = _movieRepository.QueryStringFilter(titleSearchString, perPage, orderBy);

                return Ok(movies);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
