using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
