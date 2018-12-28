using Common.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Extensions
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
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            return user;
        }
    }
}
