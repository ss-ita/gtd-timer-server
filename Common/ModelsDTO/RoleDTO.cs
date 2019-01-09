using System.ComponentModel.DataAnnotations;

namespace Common.ModelsDTO
{
    public class RoleDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
