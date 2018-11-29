using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BD
{
    public class UnityOfWork: IDisposable
    {
        private readonly DbProviderFactory _factory;
        private DbConnection _connection;
        private DbTransaction _transaction;

        public UnityOfWork(bool persistent = false)
        {
            string provider = ConfigurationManager.AppSettings["provider_mysql"];
            string connectionString = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;

            _factory = DbProviderFactories.GetFactory(provider);
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = connectionString;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public DbCommand CreateCommand(string commandText, params DbParameter[] parameters)
        {
            DbCommand command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);
            command.Transaction = _transaction;

            return command;
        }

        public DbParameter CreateParameter(DbType type, string name, object value)
        {
            var parameter = _factory.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }

        public void Complete()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
            _connection.Dispose();
        }
    }
}
