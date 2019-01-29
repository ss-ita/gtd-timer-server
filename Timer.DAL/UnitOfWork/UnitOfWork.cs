//-----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;

namespace GtdTimerDAL.UnitOfWork
{
    /// <summary>
    /// Unit of work class which implements i unit of work interface
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Timer context instance
        /// </summary>
        private readonly TimerContext context;

        /// <summary>
        /// User manager table
        /// </summary>
        private Lazy<IApplicationUserManager<User, int>> userManager;

        /// <summary>
        /// Tasks table
        /// </summary>
        private Lazy<IRepository<Tasks>> tasks;

        /// <summary>
        /// Presets table
        /// </summary>
        private Lazy<IRepository<Preset>> presets;

        /// <summary>
        /// Timers table
        /// </summary>
        private Lazy<IRepository<PresetTasks>> presetTasks;

        /// <summary>
        /// Records table
        /// </summary>
        private Lazy<IRepository<Record>> records;

        /// <summary>
        /// Alarm table
        /// </summary>
        private Lazy<IRepository<Alarm>> alarms;

        /// <summary>
        /// Users table
        /// </summary>
        private Lazy<IRepository<User>> users;

        /// <summary>
        /// Value indicating whether it is disposed 
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">context instance</param>
        /// <param name="applicationUserManager"> application user manager repository</param>
        /// <param name="role">role  repository</param>
        /// <param name="preset">preset  repository</param>
        /// <param name="timer">timer repository</param>
        /// <param name="tasks">tasks repository</param>
        /// <param name="userRole">user role repository</param>
        public UnitOfWork(TimerContext context,
            IApplicationUserManager<User, int> applicationUserManager,
            IRepository<Preset> preset,
            IRepository<PresetTasks> presetTasks,
            IRepository<Tasks> tasks,
            IRepository<Record> record,
            IRepository<Alarm> alarm,
            IRepository<User> users)
        {
            this.context = context;
            disposed = false;
            UserManager = applicationUserManager;
            Tasks = tasks;
            Presets = preset;
            PresetTasks = presetTasks;
            Records = record;
            Alarms = alarm;
            Users = users;
        }

        /// <summary>
        /// Gets or sets user manager table
        /// </summary>
        public IApplicationUserManager<User, int> UserManager
        {
            get => userManager.Value;
            set
            {
                userManager = new Lazy<IApplicationUserManager<User, int>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets tasks table
        /// </summary>
        public IRepository<Tasks> Tasks
        {
            get => tasks.Value;
            set
            {
                tasks = new Lazy<IRepository<Tasks>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets presets table
        /// </summary>
        public IRepository<Preset> Presets
        {
            get => presets.Value;
            set
            {
                presets = new Lazy<IRepository<Preset>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets timers table
        /// </summary>
        public IRepository<PresetTasks> PresetTasks
        {
            get => presetTasks.Value;
            set
            {
                presetTasks = new Lazy<IRepository<PresetTasks>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets records table
        /// </summary>
        public IRepository<Record> Records
        {
            get => records.Value;
            set
            {
                records = new Lazy<IRepository<Record>>(() => value); 
            }
        }

        /// <summary>
        /// Gets or sets alarms table
        /// </summary>
        public IRepository<Alarm> Alarms
        {
            get => alarms.Value;
            set
            {
                alarms = new Lazy<IRepository<Alarm>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets users table
        /// </summary>
        public IRepository<User> Users {
            get => users.Value;
            set
            {
                users = new Lazy<IRepository<User>>(() => value);
            }  
        }
        
        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}