namespace DEPI_Session1_WebApi.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        // Many Books => One Author
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;

        // Many-to-Many with Borrower through Loan
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
