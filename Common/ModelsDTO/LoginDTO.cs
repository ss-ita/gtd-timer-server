using System.ComponentModel.DataAnnotations;
using Common.Constant;

namespace Common.ModelsDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Password { get; set; }
    }
}
