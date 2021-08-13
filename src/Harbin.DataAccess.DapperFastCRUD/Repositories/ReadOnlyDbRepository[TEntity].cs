using Harbin.DataAccess.DapperFastCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperFastCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity>, IReadOnlyDbRepository<TEntity>
    {
        public ReadOnlyDbRepository(IReadDbConnection db) : base(db)
        {
        }
    }
}
