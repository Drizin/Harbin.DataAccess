using Harbin.DataAccess.Connections;
using System;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <summary>
    /// Factory for Read-Only DbRepositories
    /// </summary>
    public interface IReadDbRepositoryFactory
    {
        /// <summary>
        /// Creates a Read-Only DbRepository
        /// </summary>
        IReadDbRepository<TEntity> Create<TEntity>(IReadDbConnection conn);

        /// <summary>
        /// Registers a repository factory for the entity.
        /// </summary>
        void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadDbConnection, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadDbRepository<TEntity>;

        /// <summary>
        /// Registers a singleton repository for the entity.
        /// </summary>
        void RegisterSingleton<TEntity>(IReadDbRepository<TEntity> instance);
    }
}
