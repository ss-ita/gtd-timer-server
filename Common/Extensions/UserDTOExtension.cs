using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DTO;

namespace Common.Extensions
{
    public static class UserDTOExtension
    {
        public static User ToUser(this UserDTO userDTO)
        {
            User user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password
            };

            return user;
        }
    }
}
