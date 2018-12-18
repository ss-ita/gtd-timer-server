using System.ComponentModel.DataAnnotations;

namespace gtdtimer.ModelsDTO
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        
        public string Email { get; set; }
        [Required]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
