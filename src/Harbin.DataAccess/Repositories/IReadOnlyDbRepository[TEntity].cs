using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.Repositories
{
    /// <inheritdoc/>
    public interface IReadOnlyDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
    }
}
