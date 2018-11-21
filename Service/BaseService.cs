using Domain.Interfaces;
using Domain.Entities;

namespace Service
{
    public abstract class BaseService<T> where T: BaseEntity
    {
        protected IRepository<T> _repository;

        protected BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

    }
}
