using Harbin.DataAccess.DapperFastCRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperFastCRUD.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// Almost same as <see cref="IReadDbConnection"/> (doesn't add anything), but this is strictly a read-only connection, 
    /// so you can provide (and expect) a read-only connection string (for safety), and/or can point to read-replicas.
    /// </summary>
    public interface IReadOnlyDbConnection : IReadDbConnection
    {
        IReadOnlyDbRepository<TEntity> GetReadOnlyRepository<TEntity>();
    }
}
