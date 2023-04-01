using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepositoryFactory<DB> : IReadOnlyDbRepositoryFactory<DB>
    {
        private readonly Dictionary<Type, Func<IReadOnlyDbConnection<DB>, object>> _registeredTypes = new Dictionary<Type, Func<IReadOnlyDbConnection<DB>, object>>();

        /// <inheritdoc/>
        public IReadOnlyDbRepository<TEntity, DB> Create<TEntity>(IReadOnlyDbConnection<DB> conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadOnlyDbRepository<TEntity, DB>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadOnlyDbRepository<TEntity, DB>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadOnlyDbConnection<DB>, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadOnlyDbRepository<TEntity, DB>
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
