using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public interface IApplicationUserManager<TUser,TKey>
    {
        Task<IdentityResult> CreateAsync(TUser user);
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<IdentityResult> UpdateAsync(TUser user);
        Task<IdentityResult> DeleteAsync(TUser user);
        Task<TUser> FindByIdAsync(TKey userId);
        Task<TUser> FindByNameAsync(string userName);
        Task<IdentityResult> SetEmailAsync(TKey userId, string email);
        Task<TUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);
    }
}
