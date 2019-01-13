using Common.Constant;
using System.ComponentModel.DataAnnotations;

namespace Common.ModelsDTO
{
    public class UserDTO
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(Constants.PasswordRegularExpression, ErrorMessage = Constants.PasswordInvalidMessage)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
