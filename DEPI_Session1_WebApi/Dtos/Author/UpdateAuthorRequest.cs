using System.ComponentModel.DataAnnotations;

namespace DEPI_Session1_WebApi.Dtos.Author
{
    public class UpdateAuthorRequest
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
