using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <inheritdoc/>
    public interface IReadDbRepository<TEntity, DB> : IReadDbRepository<TEntity>
    {
    }
}
