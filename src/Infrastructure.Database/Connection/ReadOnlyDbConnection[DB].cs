using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Connection
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// The underlying IDbConnection can be a read-only connection (for safety) and/or can point to read-replicas.
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public class ReadOnlyDbConnection<DB> : ReadDbConnection<DB>, IReadDbConnection, IReadDbConnection<DB>, IReadOnlyDbConnection, IReadOnlyDbConnection<DB>, IDbConnection
    {
        public ReadOnlyDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
