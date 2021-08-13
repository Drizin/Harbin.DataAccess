using Harbin.DataAccess.DapperSimpleCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperSimpleCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity>, IReadOnlyDbRepository<TEntity>
    {
        public ReadOnlyDbRepository(IReadDbConnection db) : base(db)
        {
        }
    }
}
