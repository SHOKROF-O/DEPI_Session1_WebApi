namespace DEPI_Session1_WebApi.Domain
{
    public class Borrower : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }

        // Many-to-Many with Book through Loan
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
