using Dapper;
using Harbin.DataAccess.DapperSimpleCRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperSimpleCRUD.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (Queries) and Dapper Execute extensions (Commands).
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public class ReadWriteDbConnection<DB> : ReadOnlyDbConnection<DB>, IReadWriteDbConnection<DB>
    {
        public ReadWriteDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
        public ReadWriteDbConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }
        public ReadWriteDbConnection(IDbConnectionFactory<DB> connFactory) : this(connFactory.CreateConnection())
        {
        }

        #region Dapper (facades Execute())
        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Execute(sql: sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region IDbConnection facades (composition) - overrides over ReadOnlyDbConnection
        public override void ChangeDatabase(string databaseName)
        {
            DbConnection.ChangeDatabase(databaseName);
        }
        public override IDbCommand CreateCommand()
        {
            return DbConnection.CreateCommand();
        }
        #endregion

        #region GetReadWriteRepository<T>
        /// <summary>
        /// Get a Repository which you know that resides in this physical database
        /// Instead of this you can also create extensions methods to access all repositories which belong to a given physical database, like this:
        /// public static IReadWriteDbRepository<YourEntity> GetYourEntityRepository(this IReadWriteDbConnection<YourDatabase> db) => new ReadWriteDbRepository<YourEntity, YourDatabase>(db);
        /// </summary>
        public virtual IReadWriteDbRepository<TEntity, DB> GetReadWriteRepository<TEntity>()
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadWriteDbRepository<TEntity, DB>)Activator.CreateInstance(_registeredTypes[typeof(TEntity)], new object[] { this });
            return new ReadWriteDbRepository<TEntity, DB>(this);
        }

        private static Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
        /// <summary>
        /// Register a custom Repository
        /// </summary>
        public new static void RegisterRepositoryType<TEntity, TRepository>() where TRepository : IReadWriteDbRepository<TEntity>
        {
            if (typeof(TRepository).IsAbstract || typeof(TRepository).IsInterface)
                throw new ArgumentException("Cannot create instance of interface or abstract class");

            _registeredTypes.Add(typeof(TEntity), typeof(TRepository));
        }
        public new static void CleanRegisteredRepositories() => _registeredTypes.Clear();

        #endregion

    }
}
