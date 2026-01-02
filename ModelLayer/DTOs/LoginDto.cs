using System.ComponentModel.DataAnnotations;

namespace FundooNotes.API.ModelLayer.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
