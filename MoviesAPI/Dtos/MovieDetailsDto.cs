namespace MoviesAPI.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public string StoreLine { get; set; }

        public byte[] Poster { get; set; }

        public byte CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
