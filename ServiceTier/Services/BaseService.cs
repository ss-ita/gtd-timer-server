using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}