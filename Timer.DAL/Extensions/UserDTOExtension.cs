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
                UserName = userDTO.Email
            };

            return user;
        }

        public static UserDTO ToUserDTO(this User user)
        {
            UserDTO userDTO = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.PasswordHash,
                PasswordConfirm = user.PasswordHash
            };

            return userDTO;
        }
    }
}
