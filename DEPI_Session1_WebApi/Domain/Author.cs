namespace DEPI_Session1_WebApi.Domain
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // One Author => Many Books
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
