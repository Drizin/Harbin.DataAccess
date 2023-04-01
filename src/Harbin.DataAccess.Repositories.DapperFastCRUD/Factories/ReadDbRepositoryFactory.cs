using System.Collections.Generic;
using System;
using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadDbRepositoryFactory : IReadDbRepositoryFactory
    {
        private readonly Dictionary<Type, Func<IReadDbConnection, object>> _registeredTypes = new Dictionary<Type, Func<IReadDbConnection, object>>();

        /// <inheritdoc/>
        public IReadDbRepository<TEntity> Create<TEntity>(IReadDbConnection conn)
        {
            if (_registeredTypes.ContainsKey(typeof(TEntity)))
                return (IReadDbRepository<TEntity>)_registeredTypes[typeof(TEntity)].Invoke(conn);
            return new ReadDbRepository<TEntity>(conn);
        }

        /// <inheritdoc/>
        public void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadDbConnection, TRepositoryImpl> factory) 
            where TRepositoryImpl : IReadDbRepository<TEntity>
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
