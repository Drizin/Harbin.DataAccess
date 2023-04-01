using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public interface IReadOnlyDbRepository<TEntity, DB> : IReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>, IReadOnlyDbRepository<TEntity>
    {
    }
}
