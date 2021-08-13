using Harbin.DataAccess.DapperFastCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperFastCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity, DB> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>, IReadOnlyDbRepository<TEntity>, IReadOnlyDbRepository<TEntity, DB>
    {
        public ReadOnlyDbRepository(IReadDbConnection<DB> db) : base(db)
        {
        }
    }
}
