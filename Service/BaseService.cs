using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service
{
    public abstract class BaseService<T> where T: BaseEntity
    {
        protected IRepository<T> _repository;
        protected IView _view;

        public BaseService(IRepository<T> repository, IView view)
        {
            _repository = repository;
            _view = view;
        }

        public abstract void Add(T book);

        public T Get(object id)
        {
            return _repository.GetById(id);
        }
        public void Remove(T book) => _repository.Remove(book);
        public void Update(T book) => _repository.Update(book);

    }
}
