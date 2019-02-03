//-----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i base service
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// Instance of unit of work
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        /// <param name="unitOfWork"> unit of work instance </param>
        public BaseService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            this.UnitOfWork.Dispose();
        }
    }
}