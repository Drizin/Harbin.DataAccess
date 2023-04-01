using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public class ReadDbRepositoryFactory<DB> : IReadDbRepositoryFactory<DB>
    {
        private readonly Dictionary<Type, Func<IReadDbConnection<DB>, object>> _registeredTypes = new Dictionary<Type, Func<IReadDbConnection<DB>, object>>();

        /// <inheritdoc/>
        public IReadDbRepository<TEntity, DB> Create<TEntity>(IReadDbConnection<DB> conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadDbRepository<TEntity, DB>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadDbRepository<TEntity, DB>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadDbConnection<DB>, TRepositoryImpl> factory) 
            where TRepositoryImpl : IReadDbRepository<TEntity, DB>
        {
            _registeredTypes.Add(typeof(TEntity), (db) => factory(db));
        }

        /// <inheritdoc/>
        public void RegisterSingleton<TEntity>(IReadDbRepository<TEntity> instance)
        {
            _registeredTypes.Add(typeof(TEntity), (db) => instance);
        }

    }
}
