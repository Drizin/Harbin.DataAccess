using Harbin.DataAccess.DapperFastCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperFastCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadDbRepository<TEntity, DB> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>
    {
        public ReadDbRepository(IReadDbConnection<DB> db) : base(db)
        {
        }
    }
}
