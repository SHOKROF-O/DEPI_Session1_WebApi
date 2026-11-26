using System.ComponentModel.DataAnnotations;

namespace DEPI_Session1_WebApi.Dtos.Loan
{
    public class UpdateLoanRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        // send it when the book comes back
        public DateTime? ReturnDate { get; set; }
    }
}
