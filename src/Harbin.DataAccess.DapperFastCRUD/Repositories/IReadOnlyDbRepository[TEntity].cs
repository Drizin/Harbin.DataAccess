using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperFastCRUD.Repositories
{
    /// <inheritdoc/>
    public interface IReadOnlyDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
    }
}
