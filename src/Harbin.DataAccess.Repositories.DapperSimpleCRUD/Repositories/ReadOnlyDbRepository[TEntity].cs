using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity>, IReadOnlyDbRepository<TEntity>
    {
        /// <inheritdoc/>
        public ReadOnlyDbRepository(IReadDbConnection db) : base(db)
        {
        }
    }
}
