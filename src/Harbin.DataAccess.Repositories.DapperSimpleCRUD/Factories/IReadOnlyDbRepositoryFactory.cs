using Harbin.DataAccess.Connections;
using System;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <summary>
    /// Factory for Read-Only DbRepositories
    /// </summary>
    public interface IReadOnlyDbRepositoryFactory
    {
        /// <summary>
        /// Creates a Read-Only DbRepository
        /// </summary>
        IReadOnlyDbRepository<TEntity> Create<TEntity>(IReadOnlyDbConnection conn);

        /// <summary>
        /// Registers a repository factory for the entity.
        /// </summary>
        void RegisterTransient<TEntity, TRepositoryImpl>(Func<IReadOnlyDbConnection, TRepositoryImpl> factory)
            where TRepositoryImpl : IReadOnlyDbRepository<TEntity>;

        /// <summary>
        /// Registers a singleton repository for the entity.
        /// </summary>
        void RegisterSingleton<TEntity>(IReadOnlyDbRepository<TEntity> instance);
    }
}
