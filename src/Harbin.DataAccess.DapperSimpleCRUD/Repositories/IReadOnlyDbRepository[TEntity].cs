using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperSimpleCRUD.Repositories
{
    /// <inheritdoc/>
    public interface IReadOnlyDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
    }
}
