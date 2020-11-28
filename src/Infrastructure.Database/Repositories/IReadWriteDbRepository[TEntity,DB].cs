using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public interface IReadWriteDbRepository<TEntity, DB> : IReadWriteDbRepository<TEntity>
    {
        //void Insert(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
        //void Update(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
        //void Save(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
    }
}
