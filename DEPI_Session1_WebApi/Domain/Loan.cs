namespace DEPI_Session1_WebApi.Domain
{
    // Join table between Book and Borrower
    // I gave it its own Id so the same borrower can take the same book more than one time
    public class Loan : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

        public int BorrowerId { get; set; }
        public virtual Borrower Borrower { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }   // null => book still with the borrower
    }
}
