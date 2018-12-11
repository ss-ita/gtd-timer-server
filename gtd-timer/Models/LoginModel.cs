using System.ComponentModel.DataAnnotations;

using gtdtimer.Constant;

namespace gtdtimer.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Password { get; set; }


    }
}
