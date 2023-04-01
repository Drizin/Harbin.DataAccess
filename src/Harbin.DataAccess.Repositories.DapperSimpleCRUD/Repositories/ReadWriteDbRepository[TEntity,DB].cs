using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public class ReadWriteDbRepository<TEntity, DB> : ReadWriteDbRepository<TEntity>, IReadWriteDbRepository<TEntity>, IReadWriteDbRepository<TEntity, DB>
    {
        new protected readonly IReadWriteDbConnection<DB> _db;

        /// <inheritdoc/>
        public ReadWriteDbRepository(IReadWriteDbConnection<DB> db) : base(db)
        {
            _db = db;
        }
    }
}
