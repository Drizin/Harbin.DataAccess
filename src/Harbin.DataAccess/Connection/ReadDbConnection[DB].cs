using Dapper;
using Harbin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connection
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions)
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public abstract class ReadDbConnection<DB> : ReadDbConnection, IReadDbConnection, IReadDbConnection<DB>, IDbConnection
    {
        public ReadDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
        public ReadDbConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }
        public ReadDbConnection(IDbConnectionFactory<DB> connFactory) : this(connFactory.CreateConnection())
        {
        }

        #region GetReadRepository<TEntity, DB>
        /// <summary>
        /// Get a Repository which you know that resides in this physical database
        /// Instead of this you can also create extensions methods to access all repositories which belong to a given physical database, like this:
        /// public static IReadDbRepository<YourEntity> GetYourEntityRepository(this IReadDbConnection<YourDatabase> db) => new ReadDbRepository<YourEntity, YourDatabase>(db);
        /// </summary>
        public virtual new IReadDbRepository<TEntity, DB> GetReadRepository<TEntity>()
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadDbRepository<TEntity, DB>)Activator.CreateInstance(_registeredTypes[typeof(TEntity)], new object[] { this });
            return new ReadDbRepository<TEntity, DB>(this);
        }
        private static Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
        /// <summary>
        /// Register a custom Repository
        /// </summary>
        public new static void RegisterRepositoryType<TEntity, TRepository>() where TRepository : IReadDbRepository<TEntity>
        {
            if (typeof(TRepository).IsAbstract || typeof(TRepository).IsInterface)
                throw new ArgumentException("Cannot create instance of interface or abstract class");

            _registeredTypes.Add(typeof(TEntity), typeof(TRepository));
        }
        public new static void CleanRegisteredRepositories() => _registeredTypes.Clear();


        #endregion
    }
}
