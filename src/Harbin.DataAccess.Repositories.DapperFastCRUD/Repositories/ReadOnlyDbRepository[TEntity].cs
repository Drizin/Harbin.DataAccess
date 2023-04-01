using Harbin.DataAccess.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity>, IReadOnlyDbRepository<TEntity>
    {
        public ReadOnlyDbRepository(IReadDbConnection db) : base(db)
        {
        }
    }
}
