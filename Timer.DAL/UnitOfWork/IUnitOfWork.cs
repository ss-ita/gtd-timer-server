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
        /// Gets or sets tasks table
        /// </summary>
        IRepository<Tasks> Tasks { get; set; }

        /// <summary>
        /// Gets or sets presets table
        /// </summary>
        IRepository<Preset> Presets { get; set; }

        /// <summary>
        /// Gets or sets preset tasks table
        /// </summary>
        IRepository<PresetTasks> PresetTasks { get; set; }

        /// <summary>
        /// Gets or sets records table
        /// </summary>
        IRepository<Record> Records { get; set; }

        /// <summary>
        /// Gets or sets alarms table
        /// </summary>
        IRepository<Alarm> Alarms { get; set; }

        /// <summary>
        /// Method for saving changes
        /// </summary>
        void Save();
    }
}