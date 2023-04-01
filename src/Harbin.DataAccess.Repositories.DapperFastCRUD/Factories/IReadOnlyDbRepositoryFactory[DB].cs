using Harbin.DataAccess.Connections;
using System;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <summary>
    /// Factory for Read-Only DbRepositories
    /// </summary>
    public interface IReadOnlyDbRepositoryFactory<DB>
    {
        /// <summary>
        /// Creates a Read-Only DbRepository
        /// </summary>
        IReadOnlyDbRepository<TEntity, DB> Create<TEntity>(IReadOnlyDbConnection<DB> conn);

        /// <summary>
        /// Registers a repository factory for the entity.
        /// </summary>
        void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadOnlyDbConnection<DB>, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadOnlyDbRepository<TEntity, DB>;

        /// <summary>
        /// Registers a singleton repository for the entity.
        /// </summary>
        void RegisterSingleton<TEntity>(IReadOnlyDbRepository<TEntity> instance);
    }
}
