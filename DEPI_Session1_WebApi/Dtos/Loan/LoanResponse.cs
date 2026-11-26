namespace DEPI_Session1_WebApi.Dtos.Loan
{
    public class LoanResponse
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
