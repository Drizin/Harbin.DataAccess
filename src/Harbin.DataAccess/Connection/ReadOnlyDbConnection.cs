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
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// The underlying IDbConnection can be a read-only connection (for safety) and/or can point to read-replicas.
    /// </summary>
    public class ReadOnlyDbConnection : ReadDbConnection, IDbConnection
    {
        public ReadOnlyDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        #region GetReadOnlyRepository<T>
        /// <summary>
        /// Get a Repository which you know that resides in this physical database
        /// Instead of this you can also create extensions methods to access all repositories which belong to a given physical database, like this:
        /// public static IReadOnlyDbRepository<YourEntity> GetYourEntityRepository(this IReadOnlyDbConnection<YourDatabase> db) => new ReadOnlyDbRepository<YourEntity, YourDatabase>(db);
        /// </summary>
        public virtual IReadOnlyDbRepository<TEntity> GetReadOnlyRepository<TEntity>()
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadOnlyDbRepository<TEntity>)Activator.CreateInstance(_registeredTypes[typeof(TEntity)], new object[] { this });
            return new ReadOnlyDbRepository<TEntity>(this);
        }
        private static Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
        /// <summary>
        /// Register a custom Repository
        /// </summary>
        public new static void RegisterRepositoryType<TEntity, TRepository>() where TRepository : IReadOnlyDbRepository<TEntity>
        {
            if (typeof(TRepository).IsAbstract || typeof(TRepository).IsInterface)
                throw new ArgumentException("Cannot create instance of interface or abstract class");

            _registeredTypes.Add(typeof(TEntity), typeof(TRepository));
        }
        public new static void CleanRegisteredRepositories() => _registeredTypes.Clear();


        #endregion
    }
}
