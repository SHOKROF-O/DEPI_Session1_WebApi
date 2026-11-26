namespace DEPI_Session1_WebApi.Dtos.Author
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int BooksCount { get; set; }
        public List<string> BookTitles { get; set; } = new();
    }
}
