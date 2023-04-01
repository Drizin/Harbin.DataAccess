using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepositoryFactory : IReadOnlyDbRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IReadOnlyDbConnection, object>> _registeredTypes = new Dictionary<Type, Func<IReadOnlyDbConnection, object>>();

        /// <inheritdoc/>
        public IReadOnlyDbRepository<TEntity> Create<TEntity>(IReadOnlyDbConnection conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadOnlyDbRepository<TEntity>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadOnlyDbRepository<TEntity>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadOnlyDbConnection, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadOnlyDbRepository<TEntity>
        {
            _registeredTypes.Add(typeof(TEntity), (db) => factory(db));
        }

        /// <inheritdoc/>
        public void RegisterSingleton<TEntity>(IReadOnlyDbRepository<TEntity> instance)
        {
            _registeredTypes.Add(typeof(TEntity), (db) => instance);
        }
    }
}
