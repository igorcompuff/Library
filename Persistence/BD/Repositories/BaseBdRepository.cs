using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public abstract class BaseBdRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly UnityOfWork _unityOfWork;

        protected BaseBdRepository(UnityOfWork unityOfWork)
        {
            if (unityOfWork == null)
            {
                throw new ArgumentNullException("A unidade de trabalho não pode ser nula.");
            }
            _unityOfWork = unityOfWork;
        }

        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract T GetById(object id);
        public abstract IEnumerable<T> GetAll();       
    }
}
