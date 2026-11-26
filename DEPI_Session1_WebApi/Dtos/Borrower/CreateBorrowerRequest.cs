using System.ComponentModel.DataAnnotations;

namespace DEPI_Session1_WebApi.Dtos.Borrower
{
    public class CreateBorrowerRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        // if not sent we use today
        public DateTime? MembershipDate { get; set; }
    }
}
