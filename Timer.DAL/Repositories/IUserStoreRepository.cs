//-----------------------------------------------------------------------
// <copyright file="IUserStoreRepository.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Repositories
{
    /// <summary>
    /// interface for User repository class
    /// </summary>
    public interface IUserStoreRepository : IUserEmailStore<User, int>, IQueryableUserStore<User, int>, IUserPasswordStore<User, int>, IUserRoleStore<User, int>
    {
    }
}
