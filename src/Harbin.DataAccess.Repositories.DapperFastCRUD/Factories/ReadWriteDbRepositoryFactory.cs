using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadWriteDbRepositoryFactory : IReadWriteDbRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IReadWriteDbConnection, object>> _registeredTypes = new Dictionary<Type, Func<IReadWriteDbConnection, object>>();

        /// <inheritdoc/>
        public IReadWriteDbRepository<TEntity> Create<TEntity>(IReadWriteDbConnection conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadWriteDbRepository<TEntity>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadWriteDbRepository<TEntity>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadWriteDbConnection, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadWriteDbRepository<TEntity>
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
