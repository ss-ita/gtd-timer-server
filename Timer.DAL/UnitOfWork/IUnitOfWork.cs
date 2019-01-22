//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;

namespace GtdTimerDAL.UnitOfWork
{
    /// <summary>
    /// Interface for Unit of work class
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets or sets user manager table
        /// </summary>
        IApplicationUserManager<User, int> UserManager { get; set; }

        /// <summary>
        /// Gets or sets roles table
        /// </summary>
        IRepository<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets tasks table
        /// </summary>
        IRepository<Tasks> Tasks { get; set; }

        /// <summary>
        /// Gets or sets presets table
        /// </summary>
        IRepository<Preset> Presets { get; set; }

        /// <summary>
        /// Gets or sets user roles table
        /// </summary>
        IRepository<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets timers table
        /// </summary>
        IRepository<Timer> Timers { get; set; }

        /// <summary>
        /// Method for saving changes
        /// </summary>
        void Save();
    }
}