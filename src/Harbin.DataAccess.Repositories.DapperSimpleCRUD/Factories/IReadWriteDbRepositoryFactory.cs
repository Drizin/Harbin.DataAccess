using Harbin.DataAccess.Connections;
using System;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <summary>
    /// Factory for Read-Write DbRepositories
    /// </summary>
    public interface IReadWriteDbRepositoryFactory
    {
        /// <summary>
        /// Creates a Read-Write DbRepository
        /// </summary>
        IReadWriteDbRepository<TEntity> Create<TEntity>(IReadWriteDbConnection conn);

        /// <summary>
        /// Registers a repository factory for the entity.
        /// </summary>
        void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadWriteDbConnection, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadWriteDbRepository<TEntity>;

        /// <summary>
        /// Registers a singleton repository for the entity.
        /// </summary>
        void RegisterSingleton<TEntity>(IReadWriteDbRepository<TEntity> instance);
    }
}
