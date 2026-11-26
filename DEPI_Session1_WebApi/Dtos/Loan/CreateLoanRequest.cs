using System.ComponentModel.DataAnnotations;

namespace DEPI_Session1_WebApi.Dtos.Loan
{
    public class CreateLoanRequest
    {
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        [Range(1, int.MaxValue)]
        public int BorrowerId { get; set; }

        // if not sent we use now
        public DateTime? LoanDate { get; set; }
    }
}
