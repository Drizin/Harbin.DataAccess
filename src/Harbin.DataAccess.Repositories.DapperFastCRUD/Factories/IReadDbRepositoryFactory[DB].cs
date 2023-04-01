using Harbin.DataAccess.Connections;
using System;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <summary>
    /// Factory for Read-Only DbRepositories
    /// </summary>
    public interface IReadDbRepositoryFactory<DB>
    {
        /// <summary>
        /// Creates a Read-Only DbRepository
        /// </summary>
        IReadDbRepository<TEntity, DB> Create<TEntity>(IReadDbConnection<DB> conn);

        /// <summary>
        /// Registers a repository factory for the entity.
        /// </summary>
        void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadDbConnection<DB>, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadDbRepository<TEntity, DB>;

        /// <summary>
        /// Registers a singleton repository for the entity.
        /// </summary>
        void RegisterSingleton<TEntity>(IReadDbRepository<TEntity> instance);
    }
}
