//-----------------------------------------------------------------------
// <copyright file="UserDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// UserDtoExtension class for converting to user and vice versa
    /// </summary>
    public static class UserDtoExtension
    {
        /// <summary>
        /// Convert to user method
        /// </summary>
        /// <param name="userDto"> userDto model </param>
        /// <returns>returns user</returns>
        public static User ToUser(this UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email
            };
        }

        /// <summary>
        /// Convert to user method
        /// </summary>
        /// <param name="socialAuthUser"> socialUser model </param>
        /// <returns>returns user</returns>
        public static User ToUser(this BaseAuthUserData socialAuthUser)
        {
            return new User
            {
                FirstName = socialAuthUser.FirstName,
                LastName = socialAuthUser.LastName,
                Email = socialAuthUser.Email,
                UserName = socialAuthUser.Email
            };
        }
    }
}
