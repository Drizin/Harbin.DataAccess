using Harbin.Infrastructure.Database.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class ReadOnlyDbRepository<TEntity> : ReadDbRepository<TEntity>, IReadDbRepository<TEntity>, IReadOnlyDbRepository<TEntity>
    {
        public ReadOnlyDbRepository(IReadDbConnection db) : base(db)
        {
        }
    }
}
