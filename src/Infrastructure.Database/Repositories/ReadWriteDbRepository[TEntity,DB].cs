using Harbin.Infrastructure.Database.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class ReadWriteDbRepository<TEntity, DB> : ReadWriteDbRepository<TEntity>, IReadWriteDbRepository<TEntity>, IReadWriteDbRepository<TEntity, DB>
    {
        new protected readonly IReadWriteDbConnection<DB> _db;

        public ReadWriteDbRepository(IReadWriteDbConnection<DB> db) : base(db)
        {
            _db = db;
        }
    }
}
