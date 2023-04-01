using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity, DB> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>, IReadOnlyDbRepository<TEntity>, IReadOnlyDbRepository<TEntity, DB>
    {
        /// <inheritdoc/>
        public ReadOnlyDbRepository(IReadDbConnection<DB> db) : base(db)
        {
        }
    }
}
