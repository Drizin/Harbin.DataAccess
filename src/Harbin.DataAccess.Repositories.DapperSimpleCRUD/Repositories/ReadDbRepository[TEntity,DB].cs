using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public class ReadDbRepository<TEntity, DB> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>
    {
        /// <inheritdoc/>
        public ReadDbRepository(IReadDbConnection<DB> db) : base(db)
        {
        }
    }
}
