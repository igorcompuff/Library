using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        void Add(T entity);
        void Remove(T entity);
        T GetById(object id);
        IEnumerable<T> GetAll();
    }
}
