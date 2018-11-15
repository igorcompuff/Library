using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.BD.Repositories
{
    public abstract class BaseBdRepository<T> : IDisposable, IRepository<T> where T : BaseEntity
    {
        private readonly DbProviderFactory _factory;
        private readonly DbConnection _connection;
        protected BaseBdRepository()
        {
            string provider = ConfigurationManager.AppSettings["provider_mysql"];
            string connectionString = ConfigurationManager.ConnectionStrings["mysql"]?.ConnectionString;

            _factory = DbProviderFactories.GetFactory(provider);
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = connectionString;

            _connection.Open();
        }
        public void Dispose() => _connection?.Dispose();
        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract void Update(T entity);
        public abstract T GetById(object id);
        public abstract IEnumerable<T> GetAll();

        public DbParameter CreateParameter(DbType type, string name, object value)
        {
            var parameter = _factory.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }
        public DbCommand CreateCommand(string commandText, params DbParameter[] parameters)
        {
            DbCommand command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);

            return command;
        }
        protected void ExecuteNonQueryTransactional(IEnumerable<DbCommand> commands)
        {
            DbTransaction transaction = _connection.BeginTransaction();
            try
            {
                foreach (var command in commands)
                {
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    command.Dispose();
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
