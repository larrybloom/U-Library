using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
