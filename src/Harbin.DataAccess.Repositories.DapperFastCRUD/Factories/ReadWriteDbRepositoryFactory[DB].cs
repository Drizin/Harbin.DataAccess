using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadWriteDbRepositoryFactory<DB> : IReadWriteDbRepositoryFactory<DB>
    {
        private readonly Dictionary<Type, Func<IReadWriteDbConnection<DB>, object>> _registeredTypes = new Dictionary<Type, Func<IReadWriteDbConnection<DB>, object>>();

        /// <inheritdoc/>
        public IReadWriteDbRepository<TEntity, DB> Create<TEntity>(IReadWriteDbConnection<DB> conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadWriteDbRepository<TEntity, DB>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadWriteDbRepository<TEntity, DB>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadWriteDbConnection<DB>, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadWriteDbRepository<TEntity, DB>
        {
            _registeredTypes.Add(typeof(TEntity), (db) => factory(db));
        }

        /// <inheritdoc/>
        public void RegisterSingleton<TEntity>(IReadWriteDbRepository<TEntity> instance)
        {
            _registeredTypes.Add(typeof(TEntity), (db) => instance);
        }
    }
}
