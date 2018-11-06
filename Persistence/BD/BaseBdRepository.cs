using System;
using System.Collections.Generic;
using System.Data;
using Domain.Interfaces;
using System.Configuration;
using System.Data.Common;
using Domain.Entities;

namespace Persistence.BD
{
    public abstract class BaseBdRepository<T> : IRepository<T> where T: BaseEntity
    {
        protected DbProviderFactory _factory;
        protected DbConnection _connection;
        protected BaseBdRepository()
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.ConnectionStrings["sqlserver"]?.ConnectionString;

            _factory = DbProviderFactories.GetFactory(provider);
            _connection = _factory.CreateConnection();
            if (_connection == null)
            {
                throw new Exception("Não foi possível criar a conexão");
            }

            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract void Update(T entity);
        public abstract T GetById(object id);
        public abstract IEnumerable<T> GetAll();
    }
}
