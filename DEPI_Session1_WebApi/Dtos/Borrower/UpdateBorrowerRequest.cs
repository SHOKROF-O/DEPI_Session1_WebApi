using System.ComponentModel.DataAnnotations;

namespace DEPI_Session1_WebApi.Dtos.Borrower
{
    public class UpdateBorrowerRequest
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime MembershipDate { get; set; }
    }
}
