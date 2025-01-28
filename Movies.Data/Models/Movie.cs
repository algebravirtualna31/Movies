namespace Movies.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Genre { get; set; }

        public DateTime ReleaseYear { get; set; }
    }
}
